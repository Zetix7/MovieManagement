using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class AddUserHandler : IRequestHandler<AddUserRequest, AddUserResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<AddUserHandler> _logger;

    public AddUserHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<AddUserHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
    }

    public async Task<AddUserResponse> Handle(AddUserRequest request, CancellationToken token)
    {
        var entityUser = _mapper.Map<DataAccess.Entities.User>(request);
        var command = new AddUserCommand { Parameter = entityUser };
        var addedUser = await _commandExecutor.Execute(command);
        if (addedUser.Id is 0)
        {
            return new AddUserResponse { Error = new ErrorModel(ErrorType.ValidationError) };
        }

        var domainUser = _mapper.Map<User>(addedUser);
        var response = new AddUserResponse { Data = domainUser };
        return response;
    }
}
