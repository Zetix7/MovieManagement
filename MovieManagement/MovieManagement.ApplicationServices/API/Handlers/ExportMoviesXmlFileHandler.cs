using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.XmlFilesService;
using static MovieManagement.DataAccess.Entities.User;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class ExportMoviesXmlFileHandler : IRequestHandler<ExportMoviesXmlFileRequest, ExportMoviesXmlFileResponse>
{
    private readonly IXmlFileService _xmlFileService;

    public ExportMoviesXmlFileHandler(IXmlFileService xmlFileService)
    {
        _xmlFileService = xmlFileService;
    }

    public async Task<ExportMoviesXmlFileResponse> Handle(ExportMoviesXmlFileRequest request, CancellationToken cancellationToken)
    {
        if(!request.IsActiveAuthentication
            || request.AccessLevelAuthentication == Role.None.ToString())
        {
            return new ExportMoviesXmlFileResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        await _xmlFileService.ExportMoviesXmlFile();

        return new ExportMoviesXmlFileResponse();
    }
}
