using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetUsersHandler : IRequestHandler<GetUsersRequest, GetUsersResponse>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetUsersHandler> _logger;

    public GetUsersHandler(IMapper mapper, IMediator mediator, IQueryExecutor queryExecutor, ILogger<GetUsersHandler> logger)
    {
        _mapper = mapper;
        _mediator = mediator;
        _queryExecutor = queryExecutor;
        _logger = logger;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request, CancellationToken token)
    {
        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new GetUsersResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetUsersQuery();
        var users = await _queryExecutor.Execute(query);
        var domainUsers = _mapper.Map<List<User>>(users);
        var response = new GetUsersResponse { Data = domainUsers };
        return response;
    }
}
