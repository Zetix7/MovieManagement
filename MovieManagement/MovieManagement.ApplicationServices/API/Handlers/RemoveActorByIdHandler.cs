using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class RemoveActorByIdHandler : IRequestHandler<RemoveActorByIdRequest, RemoveActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;

    public RemoveActorByIdHandler(IMapper mapper, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
    }

    public async Task<RemoveActorByIdResponse> Handle(RemoveActorByIdRequest request, CancellationToken token)
    {
        var entityActor = new DataAccess.Entities.Actor { Id = request.Id };
        var command = new RemoveActorByIdCommand { Parameter = entityActor };
        var removedActor = await _commandExecutor.Execute(command);
        var domainActor = _mapper.Map<Actor>(removedActor);
        var response = new RemoveActorByIdResponse { Data = domainActor };
        return response;
    }
}
