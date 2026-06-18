using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class NewsController : Controller
    {

        private readonly FirmaContext _context;

        public NewsController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            var allNews = await _context.News.Where(n => n.IsActive).ToListAsync();
            if (allNews == null) return NotFound();

            return View(allNews);
        }

        public async Task<IActionResult> Details(int id)
        {

            var news = await  _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if(news == null) return NotFound();

            return View(news);
        }
    }
}
