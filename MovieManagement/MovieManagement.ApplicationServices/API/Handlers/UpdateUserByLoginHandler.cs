using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateUserByLoginHandler : IRequestHandler<UpdateUserByLoginRequest, UpdateUserByLoginResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<UpdateActorByIdHandler> _logger;

    public UpdateUserByLoginHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<UpdateActorByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in UpdateUserByLogin class");
    }

    public async Task<UpdateUserByLoginResponse> Handle(UpdateUserByLoginRequest request, CancellationToken token)
    {
        if (!request.IsActiveAuthentication 
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new UpdateUserByLoginResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }
        var entityUser = _mapper.Map<DataAccess.Entities.User>(request);
        var command = new UpdateUserByLoginCommand { Parameter = entityUser };
        var updatedUser = await _commandExecutor.Execute(command);
        if (updatedUser.Id is 0)
        {
            return new UpdateUserByLoginResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(updatedUser);
        var response = new UpdateUserByLoginResponse { Data = domainUser };
        return response;
    }
}
