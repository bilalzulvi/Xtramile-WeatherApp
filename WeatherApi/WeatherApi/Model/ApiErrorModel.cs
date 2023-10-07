using Newtonsoft.Json;

namespace WeatherApi.Model
{
    public class ApiErrorModel
    {
        [JsonProperty("errorMessages")]
        public string[] ErrorMessages { get; set; }
    }
}
