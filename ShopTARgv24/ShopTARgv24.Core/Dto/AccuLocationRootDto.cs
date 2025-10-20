namespace ShopTARgv24.Core.Dto
{
    public class AccuLocationRootDto
    {
        public string LocalObservationDateTime { get; set; } = string.Empty;
        public int EpochTime { get; set; }
        public string WeatherText { get; set; } = string.Empty;
        public int WeatherIcon { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; } = string.Empty;
        public bool IsDayTime { get; set; }
        public TemperatureDto? Temperature { get; set; } 
        public string MobileLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }

    public class TemperatureDto
    {
        public WeatherValueDto? Metric { get; set; }
        public WeatherValueDto? Imperial { get; set; }
    }

    public class WeatherValueDto
    {
        public double Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public int UnitType { get; set; }
    }
}
