using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMoviesHandler : IRequestHandler<GetMoviesRequest, GetMoviesResponse>
{
    private readonly IRepository<DataAccess.Entities.Movie> _movieRepository;

    public GetMoviesHandler(IRepository<DataAccess.Entities.Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public Task<GetMoviesResponse> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
    {
        var movies = _movieRepository.GetAll();
        var domainMovies = movies.Select(m => new Movie
        {
            Title = m.Title,
            Year = m.Year,
            Universe = m.Universe,
            BoxOffice = m.BoxOffice
        }).ToList();

        var response = new GetMoviesResponse()
        {
            Data = domainMovies
        };

        return Task.FromResult(response);
    }
}
