using Newtonsoft.Json;

namespace MovieManagement.ApplicationServices.Components.OpenWeather;

public class SysData
{
    [JsonProperty("country")]
    public string? Country { get; set; }
}