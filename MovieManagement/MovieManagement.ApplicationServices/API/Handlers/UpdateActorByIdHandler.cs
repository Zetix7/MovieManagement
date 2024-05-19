using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateActorByIdHandler : IRequestHandler<UpdateActorByIdRequest, UpdateActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<UpdateActorByIdHandler> _logger;

    public UpdateActorByIdHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<UpdateActorByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in UpdateActorByIdHandler class");
    }

    public async Task<UpdateActorByIdResponse> Handle(UpdateActorByIdRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in UpdateActorByIdHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new UpdateActorByIdResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }
        
        var actor = _mapper.Map<DataAccess.Entities.Actor>(request);
        var command = new UpdateActorByIdCommand { Parameter = actor };
        var updatedActor = await _commandExecutor.Execute(command);
        if (updatedActor.Id is 0)
        {
            return new UpdateActorByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainActor = _mapper.Map<Actor>(updatedActor);
        var response = new UpdateActorByIdResponse { Data = domainActor };
        return response;
    }
}
