using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class AddMovieHandler : IRequestHandler<AddMovieRequest, AddMovieResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;

    public AddMovieHandler(IMapper mapper, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
    }

    public async Task<AddMovieResponse> Handle(AddMovieRequest request, CancellationToken token)
    {
        var movie = _mapper.Map<DataAccess.Entities.Movie>(request);
        var command = new AddMovieCommand { Parameter = movie };
        var entityMovie = await _commandExecutor.Execute(command);
        var domainMovie = _mapper.Map<Movie>(entityMovie);
        var response = new AddMovieResponse { Data = domainMovie };
        return response;
    }
}
