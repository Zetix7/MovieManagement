using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class RemoveActorByIdHandler : IRequestHandler<RemoveActorByIdRequest, RemoveActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<RemoveActorByIdCommand> _logger;

    public RemoveActorByIdHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<RemoveActorByIdCommand> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in RemoveActorByIdHandler class");
    }

    public async Task<RemoveActorByIdResponse> Handle(RemoveActorByIdRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in RemoveActorByIdHandler class");
        var entityActor = new DataAccess.Entities.Actor { Id = request.Id };
        var command = new RemoveActorByIdCommand { Parameter = entityActor };
        var removedActor = await _commandExecutor.Execute(command);
        if (removedActor.Id is 0)
        {
            return new RemoveActorByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainActor = _mapper.Map<Actor>(removedActor);
        var response = new RemoveActorByIdResponse { Data = domainActor };
        return response;
    }
}
