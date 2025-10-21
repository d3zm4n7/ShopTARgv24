using Microsoft.AspNetCore.Mvc;
using ShopTARgv24.Core.ServiceInterface;
using ShopTARgv24.Models.ChuckNorris;
using System.Threading.Tasks;

namespace ShopTARgv24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController(IChuckNorrisServices chuckNorrisServices)
        {
            _chuckNorrisServices = chuckNorrisServices;
        }

        public async Task<IActionResult> Index()
        {
            var jokeDto = await _chuckNorrisServices.GetRandomJoke();

            var viewModel = new ChuckNorrisViewModel();

            if (jokeDto == null)
            {
                viewModel.Joke = "Не удалось загрузить шутку. Чак Норрис не в настроении.";
            }
            else
            {
                viewModel.Joke = jokeDto.Value;
                viewModel.IconUrl = jokeDto.IconUrl;
            }

            return View(viewModel);
        }
    }
}