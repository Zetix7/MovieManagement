using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, GetMovieByIdResponse>
{
    private readonly IRepository<DataAccess.Entities.Movie> _movieRepository;

    public GetMovieByIdHandler(IRepository<DataAccess.Entities.Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public Task<GetMovieByIdResponse> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken)
    {
        var movie = _movieRepository.GetById(request.Id);
        if(movie is null)
        {
            var failedResponse = new GetMovieByIdResponse();
            return Task.FromResult(failedResponse);
        }

        var domainMovie = new Movie()
        {
            Title = movie.Title,
            Year = movie.Year,
            Universe = movie.Universe,
            BoxOffice = movie.BoxOffice,
        };

        var response = new GetMovieByIdResponse
        {
            Data = domainMovie,
        };

        return Task.FromResult(response);
    }
}
