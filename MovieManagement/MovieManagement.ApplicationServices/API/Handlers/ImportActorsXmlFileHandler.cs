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

public class ImportActorsXmlFileHandler : IRequestHandler<ImportActorsXmlFileRequest, ImportActorsXmlFileResponse>
{
    private readonly ICommandExecutor _commandExecutor;
    private readonly IXmlFileService _xmlFileService;
    private readonly ILogger<ImportActorsXmlFileHandler> _logger;

    public ImportActorsXmlFileHandler(
        ICommandExecutor commandExecutor, IXmlFileService xmlFileService, ILogger<ImportActorsXmlFileHandler> logger)
    {
        _commandExecutor = commandExecutor;
        _xmlFileService = xmlFileService;
        _logger = logger;
        _logger.LogInformation("We are in ImportActorsXmlFileHandler class");
    }

    public async Task<ImportActorsXmlFileResponse> Handle(ImportActorsXmlFileRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in ImportActorsXmlFileHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != Role.AdministratorService.ToString())
        {
            return new ImportActorsXmlFileResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        List<Actor>? domainActors;

        try
        {
            domainActors = _xmlFileService.ImportActorsXmlFile();
        }
        catch (FileNotFoundException)
        {
            return new ImportActorsXmlFileResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }
        catch (FileLoadException)
        {
            return new ImportActorsXmlFileResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }
        catch (Exception)
        {
            return new ImportActorsXmlFileResponse { Error = new ErrorModel(ErrorType.UnsupportedMediaType) };
        }

        foreach (var actor in domainActors)
        {
            var entityActor = new DataAccess.Entities.Actor
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Movies = []
            };
            var command = new AddActorCommand { Parameter = entityActor };
            await _commandExecutor.Execute(command);
        }

        var response = new ImportActorsXmlFileResponse { Data = domainActors };
        return response;
    }
}
