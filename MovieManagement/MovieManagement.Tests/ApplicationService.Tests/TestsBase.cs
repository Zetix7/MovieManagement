namespace MovieManagement.Tests.ApplicationService.Tests;

public class TestsBase
{
    protected string? _Path;

    public TestContext? TestContext { get; set; }

    protected void SetPath(string name)
    {
        _Path = TestContext!.Properties["XmlPath"]!.ToString();
        if (_Path!.Contains("[AppPath]"))
        {
            _Path = _Path.Replace("[AppPath]",
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        if (_Path!.Contains("[FileName]"))
        {
            _Path = _Path.Replace("[FileName]", name);
        }
    }

    protected void DeleteTempXmlFile()
    {
        if (File.Exists(_Path))
        {
            File.Delete(_Path);
        }
    }
}
