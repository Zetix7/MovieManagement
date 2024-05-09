using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorByIdHandler : IRequestHandler<GetActorByIdRequest, GetActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetActorByIdHandler> _logger;

    public GetActorByIdHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetActorByIdHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetActorByIdHandler class");
    }

    public async Task<GetActorByIdResponse> Handle(GetActorByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are in Handle method in GetActorByIdHandler class");
        var query = new GetActorByIdQuery { Id = request.Id };
        var actor = await _queryExecutor.Execute(query);
        if (actor is null)
        {
            return new GetActorByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }
        var mappedActor = _mapper.Map<Actor>(actor);

        var response = new GetActorByIdResponse
        {
            Data = mappedActor
        };

        return response;
    }
}
