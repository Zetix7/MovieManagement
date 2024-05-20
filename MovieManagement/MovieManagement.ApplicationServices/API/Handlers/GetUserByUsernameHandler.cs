using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetUserByUsernameHandler : IRequestHandler<GetUserByUsernameRequest, GetUserByUsernameResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetUserByUsernameHandler> _logger;

    public GetUserByUsernameHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetUserByUsernameHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetUserByLoginHandler class");
    }

    public async Task<GetUserByUsernameResponse> Handle(GetUserByUsernameRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in GetUserByLoginHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new GetUserByUsernameResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetUserByUsernameQuery { Username = request.Username };
        var user = await _queryExecutor.Execute(query);
        if (user is null)
        {
            return new GetUserByUsernameResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(user);
        var response = new GetUserByUsernameResponse { Data = domainUser };
        return response;
    }
}
