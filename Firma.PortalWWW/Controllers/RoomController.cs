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

        public async Task<IActionResult> Index(int id)
        {

            ViewBag.SinglePageModel = await _context.Page.FindAsync(id);

            var rooms = await _context.RoomType.OrderBy(r => r.BasePrice).ToListAsync();

            return View(rooms);
        }


        public async Task<IActionResult> Details(int id)
        {

            var room = await _context.RoomType.Include(r => r.Amenities.Where(a => a.IsActive)).FirstOrDefaultAsync(r => r.Id == id);

            if (room == null) {

                return NotFound();
            }

            if (room.Amenities.Any())
            {
                ViewBag.AmenityModel = room.Amenities.Where(a => a.IsActive);
            } else
            {
                ViewBag.AmenityModel = null;
            }

            

            return View(room);
        }
    }
}
