namespace MovieManagement.ApplicationServices.Components.OpenWeather;

public interface IOpenWeatherConnector
{
    Task<OpenWeather> Connect(string city);
}
