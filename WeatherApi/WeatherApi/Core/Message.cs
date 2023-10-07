using WeatherApi.Core.Enum;

namespace WeatherApi.Core
{
    public class Message
    {
        public MessageType Type { get; set; }

        public string MessageText { get; set; } = string.Empty;
    }
}
