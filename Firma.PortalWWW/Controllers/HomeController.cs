using Firma.Data.Data;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Firma.PortalWWW.Controllers
{
    public class HomeController : Controller
    {

        private readonly FirmaContext _context;

        public HomeController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {

            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();
            ViewBag.NewsModel = await _context.News.OrderByDescending(p => p.PublishDate).Take(3).ToListAsync();

            if (id == null) id = 1;

            var item = await _context.Page.FindAsync(id);

            return View(item);
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
