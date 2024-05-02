using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class AddActorHandler : IRequestHandler<AddActorRequest, AddActorResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;

    public AddActorHandler(IMapper mapper, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
    }

    public async Task<AddActorResponse> Handle(AddActorRequest request, CancellationToken token)
    {
        var actor = _mapper.Map<DataAccess.Entities.Actor>(request);
        var command = new AddActorCommand { Parameter = actor };
        var entityActor = await _commandExecutor.Execute(command);
        var domainActor = _mapper.Map<Actor>(entityActor);
        var response = new AddActorResponse { Data = domainActor };
        return response;
    }
}
