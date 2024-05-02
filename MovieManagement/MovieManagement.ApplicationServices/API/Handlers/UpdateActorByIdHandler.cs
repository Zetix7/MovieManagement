using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateActorByIdHandler : IRequestHandler<UpdateActorByIdRequest,  UpdateActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;

    public UpdateActorByIdHandler(IMapper mapper, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
    }

    public async Task<UpdateActorByIdResponse> Handle(UpdateActorByIdRequest request, CancellationToken token)
    {
        var actor = _mapper.Map<DataAccess.Entities.Actor>(request);
        var command = new UpdateActorByIdCommand { Parameter = actor };
        var updatedActor = await _commandExecutor.Execute(command);
        var domainActor = _mapper.Map<Actor>(updatedActor);
        var response = new UpdateActorByIdResponse { Data = domainActor };
        return response;
    }
}
