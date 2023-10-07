namespace WeatherApi.Dto
{
    public class WeatherDto : BaseDto
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Location { get; set; }

        public int Time { get; set; }

        public decimal Wind { get; set; }

        public int Visibility { get; set; }

        public string SkyCondition { get; set; }

        public decimal TemperatureCelcius { get; set; }

        public decimal TemperatureFahrenheit { get; set; }

        public decimal TemperatureMaximum { get; set; }

        public decimal TemperatureMinimum { get; set; }

        public int RelativeHumidity { get; set; }

        public int Pressure { get; set; }
    }
}
