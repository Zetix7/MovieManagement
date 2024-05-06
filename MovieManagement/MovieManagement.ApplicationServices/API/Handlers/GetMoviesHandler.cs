using AutoMapper;
using MediatR;
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

    public GetMoviesHandler(IMapper mapper, IQueryExecutor queryExecutor)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
    }

    public async Task<GetMoviesResponse> Handle(GetMoviesRequest request, CancellationToken cancellationToken)
    {
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
