using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieManagement.ApplicationServices.API.Domain;

namespace MovieManagement.Controllers;

public class MoviesController : ApiControllerBase
{
    private ILogger<MoviesController> _logger { get; set; }

    public MoviesController(IMediator mediator, ILogger<MoviesController> logger) : base(mediator, logger)
    {
        _logger = logger;
        _logger.LogInformation("We are in MoviesController controller class");
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllMovies([FromQuery] GetMoviesRequest request)
    {
        _logger.LogInformation("We are in GetAllMovies method - EndPoint GET");
        return await HandleRequest<GetMoviesRequest, GetMoviesResponse>(request);
    }

    [HttpGet]
    [Route("{movieId}")]
    public async Task<IActionResult> GetMovieById([FromRoute] int movieId)
    {
        _logger.LogInformation("We are in GetMovieById method - EndPoint GET");
        var request = new GetMovieByIdRequest() { Id = movieId };
        return await HandleRequest<GetMovieByIdRequest, GetMovieByIdResponse>(request);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
    {
        _logger.LogInformation("We are in AddMovie method - EndPoint POST");
        return await HandleRequest<AddMovieRequest, AddMovieResponse>(request);
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> UpdateMovieById([FromBody] UpdateMovieByIdRequest request)
    {
        _logger.LogInformation("We are in UpdateMovieById method - EndPoint PUT");
        return await HandleRequest<UpdateMovieByIdRequest, UpdateMovieByIdResponse>(request);
    }

    [HttpDelete]
    [Route("{movieId}")]
    public async Task<IActionResult> RemoveMovieById([FromRoute] int movieId)
    {
        _logger.LogInformation("We are in RemoveMovieById method - EndPoint DELETE");
        var request = new RemoveMovieByIdRequest() { Id = movieId };
        return await HandleRequest<RemoveMovieByIdRequest, RemoveMovieByIdResponse>(request);
    }

    [HttpGet]
    [Route("export")]
    public async Task<IActionResult> ExportMoviesXmlFile(ExportMoviesXmlFileRequest request)
    {
        _logger.LogInformation("We are in ExportMoviesXmlFile method - EndPoint DELETE");
        return await HandleRequest<ExportMoviesXmlFileRequest, ExportMoviesXmlFileResponse>(request);
    }

    [HttpPost]
    [Route("import")]
    public async Task<IActionResult> ImportMoviesXmlFile(ImportMoviesXmlFileRequest request)
    {
        _logger.LogInformation("We are in ImportMoviesXmlFile method - EndPoint DELETE");
        return await HandleRequest<ImportMoviesXmlFileRequest, ImportMoviesXmlFileResponse>(request);
    }
}
