namespace WeatherApi.Model
{
    public class WeatherModel
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Location { get; set; }

        public int Time { get; set; }

        public decimal Wind { get; set; }

        public int Visibility { get; set; }

        public string SkyCondition { get; set; }

        public string TemperatureCelcius { get; set; }

        public string TemperatureFahrenheit { get; set; }

        public string DewPoint { get; set; }

        public int RelativeHumidity { get; set; }

        public int Pressure { get; set; }
    }
}
