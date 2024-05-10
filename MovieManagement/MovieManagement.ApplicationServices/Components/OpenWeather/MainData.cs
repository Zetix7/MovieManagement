using Newtonsoft.Json;

namespace MovieManagement.ApplicationServices.Components.OpenWeather;

public class MainData
{
    [JsonProperty("temp")]
    public float Temp { get; set; }
    
    [JsonProperty("temp_min")]
    public float TempMin { get; set; }
    
    [JsonProperty("temp_max")]
    public float TempMax { get; set; }
}