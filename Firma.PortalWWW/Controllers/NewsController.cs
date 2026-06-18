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

            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();



            var allNews = await _context.News.Where(n => n.IsActive).ToListAsync();
            if (allNews == null) return NotFound();

            return View(allNews);
        }

        public async Task<IActionResult> Details(int id)
        {

            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();
            ViewBag.NewsModel = await _context.News.OrderByDescending(p => p.PublishDate).Take(3).ToListAsync();

            var news = await  _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if(news == null) return NotFound();

            return View(news);
        }
    }
}
