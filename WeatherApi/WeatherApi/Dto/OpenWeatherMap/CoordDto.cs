using Newtonsoft.Json;

namespace WeatherApi.Dto.OpenWeatherMap
{
    public class CoordDto
    {
        [JsonProperty("lon")]
        public double Lon { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }
    }
}
