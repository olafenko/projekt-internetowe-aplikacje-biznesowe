using Azure.Core;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
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
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;

        public ReservationController(FirmaContext context, IRoomService roomService, IRoomTypeService roomTypeService, IReservationService reservationService, IGuestService guestService)
        {
            _context = context;
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _reservationService = reservationService;
            _guestService = guestService;
        }

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.SinglePageModel = await _context.Page.FindAsync(id);

            var defaultReservationValues = _reservationService.CreateDefaultReservationValues();

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

            if (request.CheckOutDate < request.CheckInDate)
            {
                ModelState.AddModelError("CheckOutDate","Data zameldowania musi być wcześniej niż data wymeldowania.");
                return View("Create", request);
            }

            var availabaleRooms = await _roomService.GetAvailableRooms(request.CheckInDate,request.CheckOutDate,request.AdultCount,request.ChildCount);

            ViewBag.AvailableRoomTypes = _roomTypeService.GetAvailableRoomTypes(availabaleRooms);

            return View("Create",request);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmReservation(CreateReservationDTO request, int selectedRoomTypeId)
        {
            var availabaleRooms = await _roomService.GetAvailableRooms(request.CheckInDate, request.CheckOutDate, request.AdultCount, request.ChildCount);

            //jezeli jakies bledy w formularzu to wraca do widoku create
            if (!ModelState.IsValid)
            {
                ViewBag.AvailableRoomTypes = _roomTypeService.GetAvailableRoomTypes(availabaleRooms);

                return View("Create", request);
            }


            //dodawanie gościa z modala

            var existingGuest = await _guestService.GetExistingGuestByIdentityCardNumber(request.Guest.IdentityCardNumber);

            Guest finalGuest;

            if(existingGuest != null)
            {
                finalGuest = existingGuest;
            } else
            {
                var newGuest = await _guestService.CreateNewGuest(request.Guest.Name,request.Guest.Email, request.Guest.LastName, request.Guest.PhoneNumber, request.Guest.Country, request.Guest.IdentityCardNumber);

                finalGuest = newGuest;
            }


            //do dodania filtrowanie pierwszego dostępnego pokoju do rezerwacji
            var freeRoom = _roomService.GetFirstAvailableRoomByRoomTypeId(availabaleRooms,selectedRoomTypeId);
                

            if (freeRoom != null)
            {
                request.RoomId = freeRoom.Id;
            } else
            {
                ModelState.AddModelError("RoomId", "Pokój jest już zajęty.");
            }

            await _reservationService.CreateReservation(request.CheckInDate,request.CheckOutDate,request.AdultCount,request.ChildCount,finalGuest.Id,request.RoomId);

            return RedirectToAction("Index","Home");
        }

    }

}
