using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.Cocktails;

namespace ShopTARgv24.Controllers
{
    public class CocktailController : Controller
    {
        private readonly ICocktailServices _CocktailServices;
        public CocktailController(ICocktailServices CocktailServices)
        {
            _CocktailServices = CocktailServices;
        }
        public async Task<IActionResult> Index(string searchTerm = "margarita")
        {
            var rawJson = await _CocktailServices.SearchCocktails(searchTerm);

            var viewModel = new CocktailViewModel
            {
                SearchTerm = searchTerm,
                RawData = rawJson
            };

            return View(viewModel);
        }
    }
}
