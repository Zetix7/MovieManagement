using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateUserByUsernameHandler : IRequestHandler<UpdateUserByUsernameRequest, UpdateUserByUsernameResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<UpdateActorByIdHandler> _logger;

    public UpdateUserByUsernameHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<UpdateActorByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in UpdateUserByUsernameHandler class");
    }

    public async Task<UpdateUserByUsernameResponse> Handle(UpdateUserByUsernameRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in UpdateUserByUsernameHandler class");

        if (!request.IsActiveAuthentication 
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new UpdateUserByUsernameResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }
        var entityUser = _mapper.Map<DataAccess.Entities.User>(request);
        var command = new UpdateUserByUsernameCommand { Parameter = entityUser };
        var updatedUser = await _commandExecutor.Execute(command);
        if (updatedUser.Id is 0)
        {
            return new UpdateUserByUsernameResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(updatedUser);
        var response = new UpdateUserByUsernameResponse { Data = domainUser };
        return response;
    }
}
