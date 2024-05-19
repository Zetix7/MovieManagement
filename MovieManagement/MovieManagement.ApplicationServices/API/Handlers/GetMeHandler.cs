using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMeHandler : IRequestHandler<GetMeRequest, GetMeResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger _logger;

    public GetMeHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetMeHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetMeHandler class");
    }

    public async Task<GetMeResponse> Handle(GetMeRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in GetMeHandler class");
        if (!request.IsActiveAuthentication)
        {
            return new GetMeResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetMeQuery { Login = request.UsernameAuthentication };
        var user = await _queryExecutor.Execute(query);
        if (user is null)
        {
            return new GetMeResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainUser = _mapper.Map<User>(user);
        var response = new GetMeResponse { Data = domainUser };
        return response;
    }
}
