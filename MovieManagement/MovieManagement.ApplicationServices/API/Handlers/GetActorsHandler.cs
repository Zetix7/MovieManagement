using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorsHandler : IRequestHandler<GetActorsRequest, GetActorsResponse>
{
    private readonly IRepository<DataAccess.Entities.Actor> _actorRepository;
    private readonly IMapper _mapper;

    public GetActorsHandler(IRepository<DataAccess.Entities.Actor> actorRepository, IMapper mapper)
    {
        _actorRepository = actorRepository;
        _mapper = mapper;
    }

    public Task<GetActorsResponse> Handle(GetActorsRequest request, CancellationToken cancellationToken)
    {
        var actors = _actorRepository.GetAll();
        var mappedActors = _mapper.Map<List<Actor>>(actors).ToList();

        var response = new GetActorsResponse
        {
            Data = mappedActors,
        };

        return Task.FromResult(response);
    }
}
