using AutoMapper;
using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, GetMovieByIdResponse>
{
    private readonly IRepository<DataAccess.Entities.Movie> _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieByIdHandler(IRepository<DataAccess.Entities.Movie> movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<GetMovieByIdResponse> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetById(request.Id);
        if(movie is null)
        {
            var failedResponse = new GetMovieByIdResponse();
            return failedResponse;
        }

        var mappedMovie = _mapper.Map<Movie>(movie);
        
        var response = new GetMovieByIdResponse
        {
            Data = mappedMovie,
        };

        return response;
    }
}
