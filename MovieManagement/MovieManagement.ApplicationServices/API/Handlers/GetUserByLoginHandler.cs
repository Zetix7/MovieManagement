using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetUserByLoginHandler : IRequestHandler<GetUserByLoginRequest, GetUserByLoginResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetUserByLoginHandler> _logger;

    public GetUserByLoginHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetUserByLoginHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetUserByLoginHandler class");
    }

    public async Task<GetUserByLoginResponse> Handle(GetUserByLoginRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in GetUserByLoginHandler class");

        if (!request.IsActiveAuthentication)
        {
            return new GetUserByLoginResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetUserByLoginQuery { Login = request.Login };
        var user = await _queryExecutor.Execute(query);
        if (user is null)
        {
            return new GetUserByLoginResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(user);
        var response = new GetUserByLoginResponse { Data = domainUser };
        return response;
    }
}
