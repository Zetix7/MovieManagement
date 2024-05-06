using AutoMapper;
using MediatR;
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

    public RemoveMovieByIdHandler(IMapper mapper, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
    }

    public async Task<RemoveMovieByIdResponse> Handle(RemoveMovieByIdRequest request, CancellationToken token)
    {
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
