using MediatR;

namespace MovieManagement.ApplicationServices.API.Domain;

public class ImportMoviesXmlFileRequest : RequestBase, IRequest<ImportMoviesXmlFileResponse>
{
}
