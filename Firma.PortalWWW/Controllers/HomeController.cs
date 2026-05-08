using Firma.Data.Data;
using Firma.PortalWWW.Models;
using Microsoft.AspNetCore.Mvc;
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

            ViewBag.PageModel = _context.Page.OrderBy(p => p.Position).ToList();

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
