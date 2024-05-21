using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.XmlFilesService;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class ExportActorsXmlFileHandler : IRequestHandler<ExportActorsXmlFileRequest, ExportActorsXmlFileResponse>
{
    private readonly IXmlFileService _xmlFileService;

    public ExportActorsXmlFileHandler(IXmlFileService xmlFileService)
    {
        _xmlFileService = xmlFileService;
    }

    public async Task<ExportActorsXmlFileResponse> Handle(ExportActorsXmlFileRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication == DataAccess.Entities.User.Role.None.ToString())
        {
            return new ExportActorsXmlFileResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        await _xmlFileService.ExportActorsXmlFile();

        return new ExportActorsXmlFileResponse();
    }
}
