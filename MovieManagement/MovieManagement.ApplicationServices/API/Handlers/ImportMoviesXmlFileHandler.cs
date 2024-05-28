using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.XmlFilesService;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;
using static MovieManagement.DataAccess.Entities.User;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class ImportMoviesXmlFileHandler : IRequestHandler<ImportMoviesXmlFileRequest, ImportMoviesXmlFileResponse>
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly IXmlFileService _xmlFileService;
    private readonly ILogger<ImportMoviesXmlFileHandler> _logger;

    public ImportMoviesXmlFileHandler(ICommandExecutor commandExecutor, IXmlFileService xmlFileService, ILogger<ImportMoviesXmlFileHandler> logger)
    {
        _commandExecutor = commandExecutor;
        _xmlFileService = xmlFileService;
        _logger = logger;
        _logger.LogInformation("We are in ImportMoviesXmlFileHandler class");
    }

    public async Task<ImportMoviesXmlFileResponse> Handle(ImportMoviesXmlFileRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in ImportMoviesXmlFileHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != Role.AdministratorService.ToString())
        {
            return new ImportMoviesXmlFileResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        List<Movie> domainMovies;

        try
        {
            domainMovies = _xmlFileService.ImportMoviesXmlFile(@"Resources\Files\Movies.xml");
        }
        catch (FileNotFoundException)
        {
            return new ImportMoviesXmlFileResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }
        catch (FileLoadException)
        {
            return new ImportMoviesXmlFileResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }
        catch (Exception)
        {
            return new ImportMoviesXmlFileResponse { Error = new ErrorModel(ErrorType.UnsupportedMediaType) };
        }

        foreach(var movie in domainMovies) 
        {
            var entityMovie = new DataAccess.Entities.Movie
            {
                Title = movie.Title,
                Year = movie.Year,
                Universe = movie.Universe,
                BoxOffice = movie.BoxOffice,
                Actors = []
            };

            var command = new AddMovieCommand { Parameter = entityMovie };
            await _commandExecutor.Execute(command);
        }

        return new ImportMoviesXmlFileResponse { Data = domainMovies };
    }
}
