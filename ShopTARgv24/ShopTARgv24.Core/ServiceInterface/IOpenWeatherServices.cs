﻿using ShopTARgv24.Core.Dto.OpenWeatherDto;

namespace ShopTARgv24.Core.ServiceInterface
{
    public interface IOpenWeatherServices
    {
        Task<OpenWeatherResultDto> GetOpenWeatherResult(OpenWeatherResultDto dto);
    }
}