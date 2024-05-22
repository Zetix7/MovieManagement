using MovieManagement.ApplicationServices.API.Domain.Models;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public interface IXmlFileService
{
    Task ExportUsersXmlFile();
    Task ExportActorsXmlFile();
    Task ExportMoviesXmlFile();
    List<Actor> ImportActorsXmlFile();
}
