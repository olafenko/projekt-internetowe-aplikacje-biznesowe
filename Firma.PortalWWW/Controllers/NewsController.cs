using Firma.Data.Data;
using Firma.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class NewsController : Controller
    {

        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {

            var allNews = await _newsService.GetAllNews();

            return View(allNews);
        }

        public async Task<IActionResult> Details(int id)
        {

            var news = await _newsService.GetNewsById(id);

            return View(news);
        }
    }
}
