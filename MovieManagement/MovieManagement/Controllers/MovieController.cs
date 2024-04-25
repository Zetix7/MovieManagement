using Microsoft.AspNetCore.Mvc;
using MovieManagement.DataAccess;
using MovieManagement.DataAccess.Entities;

namespace MovieManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IRepository<Movie> _movieRepository;

    public MovieController(IRepository<Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    [Route("")]
    public IEnumerable<Movie> GetAllMovies()
    {
        return _movieRepository.GetAll();
    }

    [HttpGet]
    [Route("{movieId}")]
    public Movie GetMovieById(int movieId)
    {
        return _movieRepository.GetById(movieId);
    }
}
