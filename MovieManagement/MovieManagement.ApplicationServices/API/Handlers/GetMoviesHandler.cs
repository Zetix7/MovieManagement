using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMoviesHandler : IRequestHandler<GetMoviesRequest, GetMoviesResponse>
{
    private readonly IRepository<DataAccess.Entities.Movie> _movieRepository;
    private readonly IMapper _mapper;

    public GetMoviesHandler(IRepository<DataAccess.Entities.Movie> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public Task<GetMoviesResponse> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
    {
        var movies = _movieRepository.GetAll();
        var mappedMovies = _mapper.Map<List<Movie>>(movies).ToList();

        var response = new GetMoviesResponse()
        {
            Data = mappedMovies
        };

        return Task.FromResult(response);
    }
}
