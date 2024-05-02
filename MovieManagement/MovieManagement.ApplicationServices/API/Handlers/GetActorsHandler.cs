using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorsHandler : IRequestHandler<GetActorsRequest, GetActorsResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;

    public GetActorsHandler(IMapper mapper, IQueryExecutor queryExecutor)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
    }

    public async Task<GetActorsResponse> Handle(GetActorsRequest request, CancellationToken cancellationToken)
    {
        var query = new GetActorsQuery { LastName = request.LastName };
        var actors = await _queryExecutor.Execute(query);
        var mappedActors = _mapper.Map<List<Actor>>(actors);

        var response = new GetActorsResponse
        {
            Data = mappedActors,
        };

        return response;
    }
}
