using Newtonsoft.Json;

namespace WeatherApi.Dto.OpenWeatherMap
{
    public class MainDto
    {
        [JsonProperty("temp")]
        public decimal Temp { get; set; }

        [JsonProperty("feels_like")]
        public decimal FeelsLike { get; set; }

        [JsonProperty("temp_min")]
        public decimal TempMin { get; set; }

        [JsonProperty("temp_max")]
        public decimal TempMax { get; set; }

        [JsonProperty("pressure")]
        public int Pressure { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}
