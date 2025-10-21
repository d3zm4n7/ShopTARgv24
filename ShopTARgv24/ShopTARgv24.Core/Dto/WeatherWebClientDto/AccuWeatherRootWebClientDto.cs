using Newtonsoft.Json;
using System.Text.Json.Serialization;


namespace ShopTARgv24.Core.Dto.WeatherWebClientDto
{
    public class AccuWeatherRootWebClientDto
    {
        public HeadlineDto Headline { get; set; }
        public List<DailyForecastsDto> DailyForecasts { get; set; }
    }

    public class HeadlineDto
    {
        [JsonProperty("EffectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonProperty("EffectiveEpochDate")]
        public Int64 EffectiveEpochDate { get; set; }

        [JsonProperty("Severity")]
        public int Severity { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }

        [JsonProperty("EndDate")]
        public string EndDate { get; set; }

        [JsonProperty("EndEpochDate")]
        public Int64 EndEpochDate { get; set; }

        [JsonProperty("MobileLink")]
        public string MobileLink { get; set; }

        [JsonProperty("Link")]
        public string Link { get; set; }


    }

    public class DailyForecastsDto
    {
        [JsonProperty("Date")]
        public string Date { get; set; }

        [JsonProperty("EpochDate")]
        public int EpochDate { get; set; }
        public SunDto Sun { get; set; }
        public MoonDto Moon { get; set; }
        public TemperatureDto Temperature { get; set; }
        public RealFeelTemperatureDto RealFeelTemperature { get; set; }
        public RealFeelTemperatureShadeDto RealFeelTemperatureShade { get; set; }

        [JsonProperty("HoursOfSun")]
        public float HoursOfSun { get; set; }
        public DegreeDaySummaryDto DegreeDaySummary { get; set; }
        public AirAndPollenDto AirAndPollen { get; set; }
        public DayDto Day { get; set; }
        public NightDto Night { get; set; }
    }

    public class SunDto
    {
        [JsonProperty("DailyForecasts.Sun.Rise")]
        public string Rise { get; set; }

        [JsonProperty("DailyForecasts.Sun.EpochRise")]
        public int EpochRise { get; set; }

        [JsonProperty("DailyForecasts.Sun.Set")]
        public string Set { get; set; }

        [JsonProperty("DailyForecasts.Sun.EpochSet")]
        public int EpochSet { get; set; }
    }

    public class MoonDto
    {
        [JsonProperty("DailyForecasts.Sun.Rise")]
        public string Rise { get; set; }

        [JsonProperty("DailyForecasts.Sun.EpochRise")]
        public Int64 EpochRise { get; set; }

        [JsonProperty("DailyForecasts.Sun.Set")]
        public string Set { get; set; }

        [JsonProperty("DailyForecasts.Sun.EpochSet")]
        public Int64 EpochSet { get; set; }

        [JsonProperty("DailyForecasts.Sun.Phase")]
        public string Phase { get; set; }

        [JsonProperty("DailyForecasts.Sun.Age")]
        public int Age { get; set; }
    }

    public class TemperatureDto
    {
        public MinimumDto Minimum { get; set; }
        public MaximumDto Maximum { get; set; }
    }

    public class RealFeelTemperatureDto
    {
        public MinimumDto Minimum { get; set; }
        public MaximumDto Maximum { get; set; }
    }

    public class MinimumDto
    {
        [JsonProperty("DailyForecasts.Temperature.Minimum.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.Temperature.Minimum.Unit")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.Temperature.Minimum.UnitType")]
        public int UnitType { get; set; }
    }

    public class MaximumDto
    {
        [JsonProperty("DailyForecasts.Temperature.Maximum.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.Temperature.Maximum.Unit")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.Temperature.Maximum.UnitType")]
        public int UnitType { get; set; }
    }

    public class RealFeelTemperatureShadeDto
    {
        public MinimumDto Minimum { get; set; }
        public MaximumDto Maximum { get; set; }
    }

    public class DegreeDaySummaryDto
    {
        public HeatingDto Heating { get; set; }
        public CoolingDto Cooling { get; set; }
    }

    public class HeatingDto
    {
        [JsonProperty("DailyForecasts.DegreeDaySummary.Heating.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.DegreeDaySummary.Heating.Unit")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.DegreeDaySummary.Heating.UnitType")]
        public int UnitType { get; set; }
    }

    public class CoolingDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class AirAndPollenDto
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Category { get; set; }
        public int CategoryValue { get; set; }
        public string Type { get; set; }
    }

    public class DayDto
    {
        public int Icon { get; set; }
        public string IconPhrase { get; set; }
        public LocalSourceDto LocalSource { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public string PrecipitationIntensity { get; set; }
        public string ShortPhrase { get; set; }
        public string LongPhrase { get; set; }
        public int PrecipitationProbability { get; set; }
        public int ThunderstormProbability { get; set; }
        public int RainProbability { get; set; }
        public int SnowProbability { get; set; }
        public int IceProbability { get; set; }
        public WindDto Wind { get; set; }
        public WindGustDto WindGust { get; set; }
        public TotalLiquidDto TotalLiquid { get; set; }
        public RainDto Rain { get; set; }
        public SnowDto Snow { get; set; }
        public IceDto Ice { get; set; }
        public float HoursOfPrecipitation { get; set; }
        public float HoursOfRain { get; set; }
        public int CloudCover { get; set; }
        public EvapotranspirationDto Evapotranspiration { get; set; }
        public SolarIrradianceDto SolarIrradiance { get; set; }
    }

    public class LocalSourceDto
    {
        [JsonProperty("DailyForecasts.Day.LocalSource.Id")]
        public int Id { get; set; }

        [JsonProperty("DailyForecasts.Day.LocalSource.Name")]
        public string Name { get; set; }

        [JsonProperty("DailyForecasts.Day.LocalSource.WeatherCode")]
        public string WeatherCode { get; set; }
    }

    public class WindDto
    {
        public SpeedDto Speed { get; set; }
        public DirectionDto Direction { get; set; }
    }

    public class SpeedDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class DirectionDto
    {
        [JsonProperty("DailyForecasts.Day.Wind.Direction.Degrees")]
        public double Degrees { get; set; }

        [JsonProperty("DailyForecasts.Day.Wind.Direction.Localized")]
        public string Localized { get; set; }

        [JsonProperty("DailyForecasts.Day.Wind.Direction.English")]
        public string English { get; set; }
    }

    public class WindGustDto
    {
        public SpeedDto Speed { get; set; }
    }
    //public class SpeedDto
    //{
    //    [JsonProperty("")]
    //    public double Value { get; set; }

    //    [JsonProperty("")]
    //    public string Unit { get; set; }

    //    [JsonProperty("")]
    //    public int UnitType { get; set; }
    //}

    public class TotalLiquidDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class RainDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class SnowDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class IceDto
    {
        [JsonProperty("DailyForecasts.Day.Ice.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.Day.Ice.Value")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.Day.Ice.Value")]
        public int UnitType { get; set; }
    }

    public class EvapotranspirationDto
    {
        [JsonProperty("DailyForecasts.Day.Evapotranspiration.Value")]
        public double Value { get; set; }

        [JsonProperty("DailyForecasts.Day.Evapotranspiration.Unit")]
        public string Unit { get; set; }

        [JsonProperty("DailyForecasts.Day.Evapotranspiration.UnitType")]
        public int UnitType { get; set; }
    }

    public class SolarIrradianceDto
    {
        [JsonProperty("")]
        public double Value { get; set; }

        [JsonProperty("")]
        public string Unit { get; set; }

        [JsonProperty("")]
        public int UnitType { get; set; }
    }

    public class NightDto
    {
        [JsonProperty("")]
        public int Icon { get; set; }

        [JsonProperty("")]
        public string IconPhrase { get; set; }

        [JsonProperty("")]
        public LocalSourceDto LocalSource { get; set; }

        [JsonProperty("")]
        public bool HasPrecipitation { get; set; }

        [JsonProperty("")]
        public string PrecipitationType { get; set; }

        [JsonProperty("")]
        public string PrecipitationIntensity { get; set; }

        [JsonProperty("")]
        public string ShortPhrase { get; set; }

        [JsonProperty("")]
        public string LongPhrase { get; set; }

        [JsonProperty("")]
        public int PrecipitationProbability { get; set; }

        [JsonProperty("")]
        public int ThunderstormProbability { get; set; }

        [JsonProperty("")]
        public int RainProbability { get; set; }

        [JsonProperty("")]
        public int SnowProbability { get; set; }

        [JsonProperty("")]
        public int IceProbability { get; set; }
        public WindDto Wind { get; set; }
        public WindGustDto WindGust { get; set; }
        public TotalLiquidDto TotalLiquid { get; set; }
        public RainDto Rain { get; set; }
        public SnowDto Snow { get; set; }
        public IceDto Ice { get; set; }

        [JsonProperty("")]
        public float HoursOfPrecipitation { get; set; }

        [JsonProperty("")]
        public float HoursOfRain { get; set; }

        [JsonProperty("")]
        public int CloudCover { get; set; }
        public EvapotranspirationDto Evapotranspiration { get; set; }
        public SolarIrradianceDto SolarIrradiance { get; set; }

        [JsonProperty("")]
        public string Sources { get; set; }

        [JsonProperty("")]
        public string MobileLink { get; set; }

        [JsonProperty("")]
        public string Link { get; set; }
    }
}
