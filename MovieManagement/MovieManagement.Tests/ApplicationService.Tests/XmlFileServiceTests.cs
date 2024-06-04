using System.Xml.Linq;

namespace MovieManagement.Tests.ApplicationService.Tests;

[TestClass]
public class XmlFileServiceTests : TestsBase
{
    [TestMethod]
    public void ShouldCreateOrUpdateUsersXmlFile()
    {
        // arrange
        var document = new XDocument(new XElement("Users"));
        SetPath("Users");

        // act
        document.Save(_Path!);
        TestContext!.WriteLine("Checking file: Users.xml");
        var result = File.Exists(_Path);

        // assert
        Assert.IsTrue(result);

        // delete file
        DeleteTempXmlFile();
    }

    [TestMethod]
    public void ShouldCreateOrUpdateMoviesXmlFile()
    {
        // arrange
        var document = new XDocument(new XElement("Movies"));
        SetPath("Movies");

        // act
        document.Save(_Path!);
        TestContext!.WriteLine("Checking file: Movies.xml");
        var result = File.Exists(_Path);

        // assert
        Assert.IsTrue(result);

        // delete file
        DeleteTempXmlFile();
    }

    [TestMethod]
    public void ShouldCreateOrUpdateActorsXmlFile()
    {
        // arrange
        var document = new XDocument(new XElement("Actors"));
        SetPath("Actors");

        // act
        document.Save(_Path!);
        TestContext!.WriteLine("Checking file: Actors.xml");
        var result = File.Exists(_Path);

        // assert
        Assert.IsTrue(result);

        // delete file
        DeleteTempXmlFile();
    }
}
