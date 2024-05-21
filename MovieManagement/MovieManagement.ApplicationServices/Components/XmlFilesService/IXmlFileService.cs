using MovieManagement.DataAccess;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public interface IXmlFileService
{
    Task ExportUsersXmlFile();
}
