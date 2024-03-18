using BookStore.Models;
using BookStore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }

        public async Task<IActionResult> Index(string sTerm="", int genreId=0)
        {
            IEnumerable<Product> products = await _homeRepository.GetProducts(sTerm, genreId);
            IEnumerable<Genre> genres = await _homeRepository.Genres();
            ProductDisplayModel productModel = new ProductDisplayModel
            {
                Products = products,
                Genres = genres,
                GenreId = genreId,
                STerm = sTerm
            };
            return View(productModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
