using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class RoomController : Controller
    {

        private readonly FirmaContext _context;

        public RoomController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.RoomModel = await _context.RoomType.Where(r => r.IsActive).ToListAsync();
            var pageData = await _context.Page.FirstOrDefaultAsync(p => p.IsActive && p.LinkTitle.Equals("POKOJE"));


            return View(pageData);
        }
    }
}
