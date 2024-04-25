using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorByIdHandler : IRequestHandler<GetActorByIdRequest, GetActorByIdResponse>
{
    private readonly IRepository<DataAccess.Entities.Actor> _actorRepository;

    public GetActorByIdHandler(IRepository<DataAccess.Entities.Actor> actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public Task<GetActorByIdResponse> Handle(GetActorByIdRequest request, CancellationToken cancellationToken)
    {
        var actor = _actorRepository.GetById(request.Id);
        if(actor is null)
        {
            return Task.FromResult(new GetActorByIdResponse());
        }

        var domainActor = new Actor
        {
            FirstName = actor.FirstName,
            LastName = actor.LastName
        };

        var response = new GetActorByIdResponse
        {
            Data = domainActor
        };

        return Task.FromResult(response);
    }
}
