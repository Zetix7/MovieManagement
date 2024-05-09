using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMoviesHandler : IRequestHandler<GetMoviesRequest, GetMoviesResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetMovieByIdHandler> _logger;

    public GetMoviesHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetMovieByIdHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetMoviesHandler class");
    }

    public async Task<GetMoviesResponse> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are in handle method in GetMoviesHandler class");
        var query = new GetMoviesQuery { Title = request.Title };
        var movies = await _queryExecutor.Execute(query);
        if (movies.Count is 0)
        {
            return new GetMoviesResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var mappedMovies = _mapper.Map<List<Movie>>(movies);

        var response = new GetMoviesResponse()
        {
            Data = mappedMovies
        };

        return response;
    }
}
