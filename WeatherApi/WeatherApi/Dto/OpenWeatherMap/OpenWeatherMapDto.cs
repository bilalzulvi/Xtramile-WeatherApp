using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApi.Dto.OpenWeatherMap
{
    public class OpenWeatherMapDto
    {
        [JsonProperty("coord")]
        public CoordDto Coord { get; set; }

        [JsonProperty("weather")]
        public List<WeatherDto> Weather { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("main")]
        public MainDto Main { get; set; }

        [JsonProperty("visibility")]
        public int Visibility { get; set; }

        [JsonProperty("wind")]
        public WindDto Wind { get; set; }

        [JsonProperty("clouds")]
        public CloudDto Clouds { get; set; }

        [JsonProperty("dt")]
        public int Dt { get; set; }

        [JsonProperty("sys")]
        public SysDto Sys { get; set; }

        [JsonProperty("timezone")]
        public int TimeZone { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}
