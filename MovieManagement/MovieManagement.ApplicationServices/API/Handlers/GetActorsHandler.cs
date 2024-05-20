using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorsHandler : IRequestHandler<GetActorsRequest, GetActorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetActorsHandler> _logger;

    public GetActorsHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetActorsHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetActorsHandler class");
    }

    public async Task<GetActorsResponse> Handle(GetActorsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are in Handle method in GetActorsHandler class");

        if (!request.IsActiveAuthentication)
        {
            return new GetActorsResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetActorsQuery { LastName = request.LastName };
        var actors = await _queryExecutor.Execute(query);
        if (actors.Count is 0)
        {
            return new GetActorsResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var mappedActors = _mapper.Map<List<Actor>>(actors);

        var response = new GetActorsResponse
        {
            Data = mappedActors,
        };

        return response;
    }
}
