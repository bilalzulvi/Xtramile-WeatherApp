using Newtonsoft.Json;

namespace WeatherApi.Dto.OpenWeatherMap
{
    public class CloudDto
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}
