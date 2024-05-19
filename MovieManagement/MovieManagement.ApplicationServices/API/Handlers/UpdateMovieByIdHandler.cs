using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Commands;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class UpdateMovieByIdHandler : IRequestHandler<UpdateMovieByIdRequest, UpdateMovieByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly ILogger<UpdateMovieByIdHandler> _logger;

    public UpdateMovieByIdHandler(IMapper mapper, ICommandExecutor commandExecutor, ILogger<UpdateMovieByIdHandler> logger)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _logger = logger;
        _logger.LogInformation("We are in UpdateMovieByIdHandler class");
    }

    public async Task<UpdateMovieByIdResponse> Handle(UpdateMovieByIdRequest request, CancellationToken token)
    {
        _logger.LogInformation("We are in Handle method in UpdateMovieByIdHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new UpdateMovieByIdResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }
        
        var movie = _mapper.Map<DataAccess.Entities.Movie>(request);

        if (movie is null)
        {
            return new UpdateMovieByIdResponse();
        };

        var command = new UpdateMovieByIdCommand { Parameter = movie };
        var entityMovie = await _commandExecutor.Execute(command);
        if (entityMovie.Id is 0)
        {
            return new UpdateMovieByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var domainMovie = _mapper.Map<Movie>(entityMovie);
        var response = new UpdateMovieByIdResponse { Data = domainMovie };
        return response;
    }
}
