﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdRequest, GetMovieByIdResponse>
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ILogger<GetMovieByIdHandler> _logger;

    public GetMovieByIdHandler(IMapper mapper, IQueryExecutor queryExecutor, ILogger<GetMovieByIdHandler> logger)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _logger = logger;
        _logger.LogInformation("We are in GetMovieByIdHandler class");
    }

    public async Task<GetMovieByIdResponse> Handle(GetMovieByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("We are in Handle method in GetMovieByIdHandler class");

        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication == DataAccess.Entities.User.Role.None.ToString())
        {
            return new GetMovieByIdResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        var query = new GetMovieByIdQuery { Id = request.Id };
        var movie = await _queryExecutor.Execute(query);
        if (movie is null)
        {
            return new GetMovieByIdResponse { Error = new ErrorModel(ErrorType.NotFound) };
        }

        var mappedMovie = _mapper.Map<Movie>(movie);

        var response = new GetMovieByIdResponse
        {
            Data = mappedMovie,
        };

        return response;
    }
}
