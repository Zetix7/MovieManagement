using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class AddMovieHandler : IRequestHandler<AddMovieRequest, AddMovieResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<AddMovieHandler> _logger;

    public AddMovieHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<AddMovieHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in AddMovieHandler class");
    }

    public async Task<AddMovieResponse> Handle(AddMovieRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in AddMovieHandler class");
        var movie = _mapper.Map<DataAccess.Entities.Movie>(request);
        var command = new AddMovieCommand { Parameter = movie };
        var entityMovie = await _commandExecutor.Execute(command);
        if(entityMovie.Id is 0)
        {
            return new AddMovieResponse { Error = new ErrorModel(ErrorType.ValidationError)};
        }

        var domainMovie = _mapper.Map<Movie>(entityMovie);
        var response = new AddMovieResponse { Data = domainMovie };
        return response;
    }
}
