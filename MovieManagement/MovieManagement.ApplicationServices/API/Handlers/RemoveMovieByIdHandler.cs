using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class RemoveMovieByIdHandler : IRequestHandler<RemoveMovieByIdRequest, RemoveMovieByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<RemoveMovieByIdHandler> _logger;

    public RemoveMovieByIdHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<RemoveMovieByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in RemoveMovieByIdHandler class");
    }

    public async Task<RemoveMovieByIdResponse> Handle(RemoveMovieByIdRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in RemoveMovieByIdHandler class");
        var entityMovie = new DataAccess.Entities.Movie { Id = request.Id };
        var command = new RemoveMovieByIdCommand { Parameter = entityMovie };
        var removedMovie = await _commandExecutor.Execute(command);
        if(removedMovie.Id is 0)
        {
            return new RemoveMovieByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainMovie = _mapper.Map<Movie>(removedMovie);
        var response = new RemoveMovieByIdResponse { Data = domainMovie };
        return response;
    }
}
