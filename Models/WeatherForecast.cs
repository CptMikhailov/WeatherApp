using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace WeatherApp.Models
{
    public class WeatherForecast
    {
        public City City { get; set; }
        public string Country { get; set; }
        public TimeSpan TimeZone { get; set; }
        public List<WeatherItem> List { get; set; }
    }

    public class WeatherItem
    {
        public DateTime Dt { get; set; }
        public Main Main { get; set; }
        [JsonPropertyName("dt_text")]
        public string DtText { get; set; }
        public List<Weather> Weather { get; set; }

        [JsonIgnore]
        public string WeatherIcon { get { return Weather.First().Icon; } }

        public DateTimeOffset GetLocalDateTime(TimeSpan tz) => new DateTimeOffset(Dt, tz);
    }

    public class City
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Pressure { get; set; }
        [JsonIgnore]
        public double TempF { get { return (Temp - 273.13) * 9 / 5 + 32; } }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}