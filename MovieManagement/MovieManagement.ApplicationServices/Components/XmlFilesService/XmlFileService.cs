using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;
using System.Xml.Linq;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public class XmlFileService : IXmlFileService
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;

    public XmlFileService(IMapper mapper, IQueryExecutor queryExecutor)
    {
        _mapper = mapper;
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

    public async Task ExportActorsXmlFile()
    {
        var query = new GetActorsQuery();
        var actors = await _queryExecutor.Execute(query);
        var domainActors = _mapper.Map<List<Actor>>(actors);

        var elements = new XElement("Actors", domainActors.Select(x =>
            new XElement("Actor",
                new XAttribute("FirstName", x.FirstName!),
                new XAttribute("LastName", x.LastName!),
                new XElement("MovieTitleLists", x.MovieTitleList!.Select(x =>
                    new XElement("Title", x)
                    )))));

        var document = new XDocument(elements);
        document.Save(@"Resources\Files\Actors.xml");
    }
}
