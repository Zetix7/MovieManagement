using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public interface IXmlFileService
{
    Task ExportUsersXmlFile();
    Task ExportActorsXmlFile();
    Task ExportMoviesXmlFile();
}
