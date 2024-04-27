using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorByIdHandler : IRequestHandler<GetActorByIdRequest, GetActorByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;

    public GetActorByIdHandler(IMapper mapper, IQueryExecutor queryExecutor)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
    }

    public async Task<GetActorByIdResponse> Handle(GetActorByIdRequest request, CancellationToken cancellationToken)
    {
        var query = new GetActorByIdQuery { Id =  request.Id };
        var actor = await _queryExecutor.Execute(query);
        if (actor is null)
        {
            return new GetActorByIdResponse();
        }
        var mappedActor = _mapper.Map<Actor>(actor);

        var response = new GetActorByIdResponse
        {
            Data = mappedActor
        };

        return response;
    }
}
