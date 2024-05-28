using AutoMapper;
using MovieManagement.ApplicationServices.API.Domain.Models;
using MovieManagement.DataAccess.CQRS;
using MovieManagement.DataAccess.CQRS.Queries;
using System.Globalization;
using System.Xml.Linq;

namespace MovieManagement.ApplicationServices.Components.XmlFilesService;

public class XmlFileService : IXmlFileService
{
    private readonly IMapper _mapper;
    private readonly IQueryExecutor _queryExecutor;
    private readonly ICommandExecutor _commandExecutor;

    public XmlFileService(IMapper mapper, IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
    {
        _mapper = mapper;
        _queryExecutor = queryExecutor;
        _commandExecutor = commandExecutor;
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
                new XAttribute("Id", x.Id!),
                new XAttribute("FirstName", x.FirstName!),
                new XAttribute("LastName", x.LastName!),
                new XElement("MovieTitleLists", x.MovieTitleList!.Select(x =>
                    new XElement("Title", x)
                    )))));

        var document = new XDocument(elements);
        document.Save(@"Resources\Files\Actors.xml");
    }

    public async Task ExportMoviesXmlFile()
    {
        var query = new GetMoviesQuery();
        var movies = await _queryExecutor.Execute(query);
        var domainMovies = _mapper.Map<List<Movie>>(movies);

        var elements = new XElement("Movies", domainMovies.Select(x =>
            new XElement("Movie",
                new XAttribute("Id", x.Id!),
                new XAttribute("Title", x.Title!),
                new XAttribute("Year", x.Year!),
                new XAttribute("Universe", x.Universe!),
                new XAttribute("BoxOffice", x.BoxOffice),
                new XElement("Cast", x.Cast!.Select(x =>
                    new XElement("Actor", x))))));

        var document = new XDocument(elements);
        document.Save(@"Resources\Files\Movies.xml");
    }

    public List<Actor> ImportActorsXmlFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Not Found 'Actors.xml' file!");
        }

        if (new FileInfo(path).Length < 1)
        {
            throw new FileLoadException("File 'Actors.xml' is empty!");
        }

        List<Actor> actors;
        try
        {
            var xmlActors = XDocument.Load(path);

            actors = xmlActors.Element("Actors")!
                .Elements("Actor").Select(x => new Actor
                {
                    FirstName = x.Attribute("FirstName")!.Value,
                    LastName = x.Attribute("LastName")!.Value
                }).ToList();
        }
        catch (Exception)
        {
            throw new Exception("File 'Actors.xml' is broken!");
        }

        return actors;
    }

    public List<Movie> ImportMoviesXmlFile(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Not Found 'Movies.xml' file!");
        }

        if (new FileInfo(path).Length < 1)
        {
            throw new FileLoadException("File 'Movies.xml' is empty!");
        }

        List<Movie> movies;
        try
        {
            var xmlActors = XDocument.Load(path);

            movies = xmlActors.Element("Movies")!
                .Elements("Movie").Select(x => new Movie
                {
                    Title = x.Attribute("Title")!.Value,
                    Year = int.Parse(x.Attribute("Year")!.Value, CultureInfo.InvariantCulture),
                    Universe = x.Attribute("Universe")!.Value,
                    BoxOffice = decimal.Parse(x.Attribute("BoxOffice")!.Value, CultureInfo.InvariantCulture)
                }).ToList();
        }
        catch (Exception)
        {
            throw new Exception("File 'Movies.xml' is broken!");
        }

        return movies;
    }
}
