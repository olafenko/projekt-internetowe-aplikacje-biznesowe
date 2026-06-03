using Azure.Core;
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

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();
            ViewBag.SinglePageModel = await _context.Page.FindAsync(id);

            var defaultReservationValues = new Reservation();

            defaultReservationValues.CheckInDate = DateTime.Now;
            defaultReservationValues.CheckOutDate = defaultReservationValues.CheckInDate.AddDays(1);

            return View(defaultReservationValues);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckInDate,CheckOutDate,AdultCount,ChildCount")] Reservation reservation)
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
            ViewBag.NewsModel = await _context.News.OrderByDescending(p => p.PublishDate).Take(3).ToListAsync();

            if (request.CheckOutDate < request.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate","Data zameldowania musi być wcześniej niż data wymeldowania.");
                return View("Create", request);
            }

            ViewBag.AvailableRoomTypes = getAvailableRoomTypesAsync(request.CheckInDate,request.CheckOutDate,request.AdultCount,request.ChildCount);

            return View("Create",request);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation([Bind("CheckInDate,CheckOutDate,AdultCount,ChildCount,Guest.Name,Guest.LastName,Guest.Email,Guest.Country,Guest.PhoneNumber,Guest.IdentityCardNumber")] Reservation reservation, int selectedRoomTypeId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AvailableRoomTypes = getAvailableRoomTypesAsync(reservation.CheckInDate, reservation.CheckOutDate, reservation.AdultCount, reservation.ChildCount);
                return View("Create",reservation);
            }


            //dodawanie gościa z modala
            var currentGuest = reservation.Guest;

            var existingGuest = await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.IdentityCardNumber == reservation.Guest.IdentityCardNumber);

            if(existingGuest != null)
            {
                reservation.GuestId = existingGuest.Id;
                reservation.Guest = null;
            } else
            {
                _context.Guest.Add(currentGuest);
                await _context.SaveChangesAsync();

                reservation.GuestId = currentGuest.Id;
                reservation.Guest = null;
            }


            //do dodania filtrowanie pierwszego dostępnego pokoju do rezerwacji


            _context.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private async Task<IEnumerable<RoomType>> getAvailableRoomTypesAsync(DateTime checkIn, DateTime checkOut,int adults, int children )
        {
            var availableRooms = await _context.Room.Include(r => r.RoomType).Where(r => r.IsActive && r.RoomType.MaxGuests >= (adults + children))
                .Where(r => !r.Reservations.Any(rs => rs.CheckOutDate > checkIn && rs.CheckInDate < checkOut)).ToListAsync();

            return availableRooms.Select(r => r.RoomType).DistinctBy(rt => rt.Id);

        }

    }

}
