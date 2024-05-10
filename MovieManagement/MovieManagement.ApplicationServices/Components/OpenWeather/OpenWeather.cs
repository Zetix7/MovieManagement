using Newtonsoft.Json;

namespace MovieManagement.ApplicationServices.Components.OpenWeather;

public class OpenWeather
{
    public string? Name { get; set; }

    [JsonProperty("sys")]
    public SysData? Sys { get; set; }
    
    [JsonProperty("main")]
    public MainData? Main {  get; set; }
}
