using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class AddActorHandler : IRequestHandler<AddActorRequest, AddActorResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<AddActorHandler> _logger;

    public AddActorHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<AddActorHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in AddActorHandler class");
    }

    public async Task<AddActorResponse> Handle(AddActorRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in AddActorHandler class");
        var actor = _mapper.Map<DataAccess.Entities.Actor>(request);
        var command = new AddActorCommand { Parameter = actor };
        var entityActor = await _commandExecutor.Execute(command);
        if (entityActor.Id == 0)
        {
            return new AddActorResponse { Error = new ErrorModel(ErrorType.ValidationError) };
        }

        var domainActor = _mapper.Map<Actor>(entityActor);
        var response = new AddActorResponse { Data = domainActor };
        return response;
    }
}
