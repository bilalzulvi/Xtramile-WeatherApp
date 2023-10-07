using Newtonsoft.Json;

namespace WeatherApi.Dto
{
    public class ApiErrorDto
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
