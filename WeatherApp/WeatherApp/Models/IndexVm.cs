using Microsoft.AspNetCore.Mvc.Rendering;

namespace WeatherApp.Models
{
    public class IndexVm
    {
        public string Country { get; set; }

        public SelectList CountryList { get; set; }

        public string City { get; set; }

        public SelectList CityList { get; set; }
    }
}
