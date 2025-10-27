using Microsoft.AspNetCore.Mvc;

namespace ShopTARgv24.Controllers
{
    public class CocktailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
