using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.Dto.OpenWeatherDto;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.OpenWeather;

namespace ShopTARgv24.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IOpenWeatherServices _openWeatherServices;

        public OpenWeatherController(IOpenWeatherServices openWeatherServices)
        {
            _openWeatherServices = openWeatherServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Post meetod linna otsimiseks
        [HttpPost]
        public IActionResult SearchCity(OpenWeatherSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "OpenWeather", new { city = model.Name });
            }

            return View();
        }

        // Get meetod linna ilmastiku kuvamiseks
        [HttpGet]
        public IActionResult City(string city)
        {
            OpenWeatherResultDto dto = new();
            dto.Name = city;

            _openWeatherServices.GetOpenWeatherResult(dto);

            OpenWeatherViewModel vm = new();

            vm.Name = dto.Name;
            vm.Coord = dto.Coord;
            vm.Weather = dto.Weather;
            vm.Base = dto.Base;
            vm.Main = dto.Main;
            vm.Visibility = dto.Visibility;
            vm.Wind = dto.Wind;
            vm.Clouds = dto.Clouds;
            vm.Dt = dto.Dt;
            vm.Sys = dto.Sys;
            vm.Timezone = dto.Timezone;
            vm.Id = dto.Id;
            vm.Cod = dto.Cod;

            vm.All = dto.All;
            vm.Lon = dto.Lon;
            vm.Lat = dto.Lat;
            vm.Temp = dto.Temp;
            vm.feels_like = dto.feels_like;
            vm.TempMin = dto.TempMin;
            vm.TempMax = dto.TempMax;
            vm.Pressure = dto.Pressure;
            vm.Humidity = dto.Humidity;
            vm.SeaLevel = dto.SeaLevel;
            vm.GrndLevel = dto.GrndLevel;
            vm.Type = dto.Type;
            vm.IdSys = dto.IdSys;
            vm.Country = dto.Country;
            vm.Sunrise = dto.Sunrise;
            vm.Sunset = dto.Sunset;
            vm.IdWeather = dto.IdWeather;
            vm.MainWeather = dto.MainWeather;
            vm.Description = dto.Description;
            vm.Icon = dto.Icon;
            vm.Speed = dto.Speed;
            vm.Deg = dto.Deg;

            return View(vm);
        }
    }
}