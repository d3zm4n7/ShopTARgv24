using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services
{
    
    public class CocktailServices : ICocktailServices
    {
        private readonly HttpClient _httpClient;
                public CocktailServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> SearchCocktails(string searchTerm)
        {
            string apiUrl = $"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={searchTerm}";

            var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("User-Agent", "ShopTARgv24-App");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                // Просто читаем ответ как строку
                string rawJsonString = await response.Content.ReadAsStringAsync();
                return rawJsonString;
            }

            return $"Error: couldn't get a data. Status: {response.StatusCode}";
        }

    }
}
