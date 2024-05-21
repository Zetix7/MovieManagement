using MediatR;
using MovieManagement.ApplicationServices.API.Domain;
using MovieManagement.ApplicationServices.API.ErrorHandling;
using MovieManagement.ApplicationServices.Components.XmlFilesService;

namespace MovieManagement.ApplicationServices.API.Handlers;

public class ExportUsersXmlFileHandler : IRequestHandler<ExportUsersXmlFileRequest, ExportUsersXmlFileResponse>
{
    private readonly IXmlFileService _xmlFileService;

    public ExportUsersXmlFileHandler(IXmlFileService xmlFileService)
    {
        _xmlFileService = xmlFileService;
    }

    public async Task<ExportUsersXmlFileResponse> Handle(ExportUsersXmlFileRequest request, CancellationToken cancellationToken)
    {
        if (!request.IsActiveAuthentication
            || request.AccessLevelAuthentication != DataAccess.Entities.User.Role.AdministratorService.ToString())
        {
            return new ExportUsersXmlFileResponse { Error = new ErrorModel(ErrorType.Unauthorized) };
        }

        await _xmlFileService.ExportUsersXmlFile();

        return new ExportUsersXmlFileResponse();
    }
}
