using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetActorByIdHandler : IRequestHandler<GetActorByIdRequest, GetActorByIdResponse>
{
    private readonly IRepository<DataAccess.Entities.Actor> _actorRepository;
    private readonly IMapper _mapper;

    public GetActorByIdHandler(IRepository<DataAccess.Entities.Actor> actorRepository, IMapper mapper)
    {
        _actorRepository = actorRepository;
        _mapper = mapper;
    }

    public async Task<GetActorByIdResponse> Handle(GetActorByIdRequest request, CancellationToken cancellationToken)
    {
        var actor = await _actorRepository.GetById(request.Id);
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
