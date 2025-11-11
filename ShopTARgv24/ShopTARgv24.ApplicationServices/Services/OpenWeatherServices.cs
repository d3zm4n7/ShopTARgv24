using Nancy.Json;
using ShopTARgv24.Core.Dto.OpenWeatherDto;
using ShopTARgv24.Core.ServiceInterface;
using System.Net;

namespace ShopTARgv24.ApplicationServices.Services
{
    public class OpenWeatherServices : IOpenWeatherServices
    {
        public async Task<OpenWeatherResultDto> GetOpenWeatherResult(OpenWeatherResultDto dto)
        {
            string apiKey = "35925013ba28079b054c94b9489ae1cd";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.Name}&appid={apiKey}&units=metric";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                System.Diagnostics.Debug.WriteLine(json);

                OpenWeatherRootDto openWeatherRootDto = new JavaScriptSerializer()
                    .Deserialize<OpenWeatherRootDto>(json);

                dto.Coord = openWeatherRootDto.Coord;
                dto.Weather = openWeatherRootDto.Weather;
                dto.Base = openWeatherRootDto.Base;
                dto.Main = openWeatherRootDto.Main;
                dto.Visibility = openWeatherRootDto.Visibility;
                dto.Wind = openWeatherRootDto.Wind;
                dto.Clouds = openWeatherRootDto.Clouds;
                dto.Dt = openWeatherRootDto.Dt;
                dto.Sys = openWeatherRootDto.Sys;
                dto.Timezone = openWeatherRootDto.Timezone;
                dto.Id = openWeatherRootDto.Id;
                dto.Name = openWeatherRootDto.Name;
                dto.Cod = openWeatherRootDto.Cod;

                dto.All = openWeatherRootDto.Clouds.All;

                dto.Lon = openWeatherRootDto.Coord.Lon;
                dto.Lat = openWeatherRootDto.Coord.Lat;

                dto.Temp = openWeatherRootDto.Main.Temp;
                dto.feels_like = openWeatherRootDto.Main.feels_like;
                dto.TempMin = openWeatherRootDto.Main.TempMin;
                dto.TempMax = openWeatherRootDto.Main.TempMax;
                dto.Pressure = openWeatherRootDto.Main.Pressure;
                dto.Humidity = openWeatherRootDto.Main.Humidity;
                dto.SeaLevel = openWeatherRootDto.Main.SeaLevel;
                dto.GrndLevel = openWeatherRootDto.Main.GrndLevel;

                dto.Type = openWeatherRootDto.Sys.Type;
                dto.IdSys = openWeatherRootDto.Sys.Id;
                dto.Country = openWeatherRootDto.Sys.Country;
                dto.Sunrise = openWeatherRootDto.Sys.Sunrise;
                dto.Sunset = openWeatherRootDto.Sys.Sunset;

                dto.IdWeather = openWeatherRootDto.Weather[0].Id;
                dto.MainWeather = openWeatherRootDto.Weather[0].Main;
                dto.Description = openWeatherRootDto.Weather[0].Description;
                dto.Icon = openWeatherRootDto.Weather[0].Icon;

                dto.Speed = openWeatherRootDto.Wind.Speed;
                dto.Deg = openWeatherRootDto.Wind.Deg;
            }

            return dto;
        }
    }
}