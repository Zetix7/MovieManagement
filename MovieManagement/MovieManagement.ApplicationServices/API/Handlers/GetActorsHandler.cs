using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorsHandler : IRequestHandler<GetActorsRequest, GetActorsResponse>
{
    private readonly IRepository<DataAccess.Entities.Actor> _actorRepository;

    public GetActorsHandler(IRepository<DataAccess.Entities.Actor> actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public Task<GetActorsResponse> Handle(GetActorsRequest request, CancellationToken cancellationToken)
    {
        var actors = _actorRepository.GetAll();
        var domainActors = actors.Select(a=>new Actor
        {
            FirstName = a.FirstName,
            LastName = a.LastName
        }).ToList();

        var response = new GetActorsResponse
        {
            Data = domainActors,
        };

        return Task.FromResult(response);
    }
}
