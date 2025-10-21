namespace ShopTARgv24.Models.Weather
{
    public class AccuWeatherViewModel
    {
        public string CityName { get; set; } = string.Empty;
        //public string CityCode { get; set; }
        //public int Rank { get; set; }


        public string EffectiveDate { get; set; } = string.Empty;
        public Int64 EffectiveEpochDate { get; set; }
        public int Severity { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public Int64 EndEpochDate { get; set; }


        public string DailyForecastsDate { get; set; } = string.Empty;
        public int DailyForecastsEpochDate { get; set; }

        public double TempMinValue { get; set; }
        public string TempMinUnit { get; set; } = string.Empty;
        public int TempMinUnitType { get; set; }

        public double TempMaxValue { get; set; }
        public string TempMaxUnit { get; set; } = string.Empty;
        public int TempMaxUnitType { get; set; }

        public int DayIcon { get; set; }
        public string DayIconPhrase { get; set; } = string.Empty;
        public bool DayHasPrecipitation { get; set; }
        public string DayPrecipitationType { get; set; } = string.Empty;
        public string DayPrecipitationIntensity { get; set; } = string.Empty;

        public int NightIcon { get; set; }
        public string NightIconPhrase { get; set; } = string.Empty;
        public bool NightHasPrecipitation { get; set; }
        public string NightPrecipitationType { get; set; } = string.Empty;
        public string NightPrecipitationIntensity { get; set; } = string.Empty;

        public string MobileLink { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
