using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

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
            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();

            var defaultReservationValues = new Reservation();

            defaultReservationValues.CheckInDate = DateTime.Now;
            defaultReservationValues.CheckOutDate = defaultReservationValues.CheckInDate.AddDays(1);

            return View(defaultReservationValues);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckInDate,CheckOutDate,GuestId,AdultCount,ChildCount")] Reservation reservation)
        {

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }
        [HttpPost]
        public async Task<IActionResult> SearchAvailable([Bind("CheckInDate,CheckOutDate,AdultCount,ChildCount")] Reservation request)
        {

            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();

            if (request.CheckOutDate < request.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate","Data zameldowania musi być wcześniej niż data wymeldowania.");
                return View("Create", request);
            }

            var availableRooms = await _context.Room.Include(r => r.RoomType).Where(r => r.IsActive && r.RoomType.MaxGuests >= (request.AdultCount + request.ChildCount))
                .Where(r => !r.Reservations.Any(rs => rs.CheckOutDate > request.CheckInDate && rs.CheckInDate < request.CheckOutDate)).ToListAsync();

            var availableRoomTypes = availableRooms.Select(r => r.RoomType).DistinctBy(rt =>rt.Id);


            ViewBag.AvailableRoomTypes = availableRoomTypes;

            return View("Create",request);
        }


        public async Task<IActionResult> GuestDetails(Reservation reservation)
        {
            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();

            return View("Create",reservation);
        }





    }
}
