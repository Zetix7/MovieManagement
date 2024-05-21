using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;
using System.Xml.Linq;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public class XmlFileService : IXmlFileService
{
    private readonly IQueryExecutor _queryExecutor;

    public XmlFileService(IQueryExecutor queryExecutor)
    {
        _queryExecutor = queryExecutor;
    }

    public async Task ExportUsersXmlFile()
    {
        var query = new GetUsersQuery();
        var users = await _queryExecutor.Execute(query);

        var elements = new XElement("Users", users.Select(x =>
            new XElement("User", 
                new XAttribute("Id", x.Id),
                new XAttribute("FirstName", x.FirstName!),
                new XAttribute("LastName", x.LastName!),
                new XAttribute("Username", x.Username!),
                new XAttribute("AccessLevel", x.AccessLevel!),
                new XAttribute("IsActive", x.IsActive!)
                )));

        var document = new XDocument(elements);
        document.Save(@"Resources\Files\Users.xml");
    }
}
