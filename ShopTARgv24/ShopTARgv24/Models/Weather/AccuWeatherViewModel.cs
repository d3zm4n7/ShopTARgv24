namespace ShopTARgv24.Models.Weather
{
    public class AccuWeatherViewModel
    {
        public string CityName { get; set; }
        public DateTime EndDate { get; set; }
        public string Text { get; set; } = string.Empty;
        public double TempMetricValueUnit { get; set; }
    }
}
