using Newtonsoft.Json;
using RestSharp;

namespace MovieManagement.ApplicationServices.Components.OpenWeather;

public class OpenWeatherConnector : IOpenWeatherConnector
{
    private readonly RestClient restClient;
    private readonly string baseUrl = "http://api.openweathermap.org/";
    private readonly string apikey = "4e88e9343675f8b0274082aa106f878c";
    private readonly string units = "metric";

    public OpenWeatherConnector()
    {
        restClient = new RestClient(baseUrl);
    }

    public async Task<OpenWeather> Connect(string city)
    {
        var request = new RestRequest("data/2.5/weather", Method.Get);
        request.AddParameter("q", city);
        request.AddParameter("appid", apikey);
        request.AddParameter("units", units);
        var queryResult = await restClient.ExecuteAsync(request);
        if(queryResult.Content is "{\"cod\":\"404\",\"message\":\"city not found\"}")
        {
            return null!;
        }

        var openWeather = JsonConvert.DeserializeObject<OpenWeather>(queryResult.Content!);
        return openWeather!;
    }
}
