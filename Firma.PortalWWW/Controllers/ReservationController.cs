using Azure.Core;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.PortalWWW.DTO_s;
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

            var defaultReservationValues = new CreateReservationDTO();

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
        public async Task<IActionResult> SearchAvailable([Bind("CheckInDate,CheckOutDate,AdultCount,ChildCount")] CreateReservationDTO request)
        {

            ViewBag.PageModel = await _context.Page.OrderBy(p => p.Position).ToListAsync();
            ViewBag.NewsModel = await _context.News.OrderByDescending(p => p.PublishDate).Take(3).ToListAsync();

            if (request.CheckOutDate < request.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate","Data zameldowania musi być wcześniej niż data wymeldowania.");
                return View("Create", request);
            }

            var availabaleRooms = await getAvailableRooms(request.CheckInDate, request.CheckOutDate, request.AdultCount, request.ChildCount).ToListAsync();

            ViewBag.AvailableRoomTypes = availabaleRooms.Select(r => r.RoomType).DistinctBy(rt => rt.Id);

            return View("Create",request);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation(CreateReservationDTO request, int selectedRoomTypeId)
        {


            //jezeli jakies bledy w formularzu to wraca do widoku create
            if (!ModelState.IsValid)
            {

                ViewBag.AvailableRoomTypes = getAvailableRooms(request.CheckInDate, request.CheckOutDate, request.AdultCount, request.ChildCount)
                    .Select(r => r.RoomType).DistinctBy(rt => rt.Id);

                return View("Create", request);
            }


            //dodawanie gościa z modala

            var existingGuest = await _context.Guest.FirstOrDefaultAsync(g => g.IsActive && g.IdentityCardNumber == request.Guest.IdentityCardNumber);

            int finalGuestId;

            if(existingGuest != null)
            {
                finalGuestId = existingGuest.Id;
            } else
            {
                var newGuest = new Guest{
                    Name = request.Guest.Name,
                    LastName = request.Guest.LastName,
                    Email = request.Guest.Email,
                    PhoneNumber = request.Guest.PhoneNumber,
                    Country = request.Guest.Country,
                    IdentityCardNumber = request.Guest.IdentityCardNumber,
                    IsActive = true
                };

                _context.Guest.Add(newGuest);
                _context.SaveChangesAsync();

                finalGuestId = newGuest.Id;
            }


            //do dodania filtrowanie pierwszego dostępnego pokoju do rezerwacji
            var freeRoom = getAvailableRooms(request.CheckInDate, request.CheckOutDate, request.AdultCount, request.ChildCount)
                .FirstOrDefaultAsync(r => r.IsActive && r.RoomTypeId == selectedRoomTypeId);

            if (freeRoom != null)
            {
                request.RoomId = freeRoom.Id;
            } else
            {
                ModelState.AddModelError("RoomId", "Pokój jest już zajęty.");
            }

            var finalReservation = new Reservation
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                AdultCount = request.AdultCount,
                ChildCount = request.ChildCount,
                GuestId = finalGuestId,
                RoomId = request.RoomId
            };

            _context.Add(finalReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private IQueryable<Room> getAvailableRooms(DateTime checkIn, DateTime checkOut,int adults, int children )
        {
            return _context.Room.Include(r => r.RoomType).Where(r => r.IsActive && r.RoomType.MaxGuests >= (adults + children))
                .Where(r => !r.Reservations.Any(rs => rs.CheckOutDate > checkIn && rs.CheckInDate < checkOut)).AsQueryable();
        }

    }

}
