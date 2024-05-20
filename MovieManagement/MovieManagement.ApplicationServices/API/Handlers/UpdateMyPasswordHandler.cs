using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.PassworHasher;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateMyPasswordHandler : IRequestHandler<UpdateMyPasswordRequest, UpdateMyPasswordResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UpdateActorByIdHandler> _logger;

    public UpdateMyPasswordHandler(
        IMapper mapper,
        ICommandExecutor commandExecutor,
        IPasswordHasher passwordHasher,
        ILogger<UpdateActorByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _passwordHasher = passwordHasher;
        _logger = logger;
        _logger.LogInformation("We are in UpdateMyPasswordHandler class");
    }

    public async Task<UpdateMyPasswordResponse> Handle(UpdateMyPasswordRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in UpdateMyPasswordHandler class");
        if (!request.IsActiveAuthentication)
        {
            return new UpdateMyPasswordResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var entityUser = new DataAccess.Entities.User
        {
            Username = request.UsernameAuthentication,
            Password = _passwordHasher.Hash(request.Password!)
        };
        var command = new UpdateMyPasswordCommand { Parameter = entityUser };
        var updatedUser = await _commandExecutor.Execute(command);
        if (updatedUser.Id == 0)
        {
            return new UpdateMyPasswordResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(updatedUser);
        var response = new UpdateMyPasswordResponse { Data = domainUser };
        return response;
    }
}
