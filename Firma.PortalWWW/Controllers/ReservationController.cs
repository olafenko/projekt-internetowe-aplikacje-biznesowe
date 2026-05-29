using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class ReservationController : Controller
    {

        private readonly FirmaContext _context;

        public ReservationController(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create()
        {

            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Number");
            ViewData["RoomTypes"] = new SelectList(_context.RoomType, "Id", "Name");
            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckInDate,CheckOutDate,GuestId,AdultCount,ChildCount,RoomId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public async Task<IActionResult> SearchAvailable([Bind("CheckInDate,CheckOutDate,AdultCount,ChildCount")] Reservation reservation)
        {

            ViewBag.AvailableRooms = await _context.Page.OrderBy(p => p.Position).ToListAsync();

            return View();
        }

    }
}
