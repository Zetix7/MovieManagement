using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class ExportMoviesXmlFileRequest : RequestBase, IRequest<ExportMoviesXmlFileResponse>
{
}
