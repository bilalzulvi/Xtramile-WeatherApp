using Newtonsoft.Json;

namespace WeatherApi.Dto.OpenWeatherMap
{
    public class SysDto
    {

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("subrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }
}
