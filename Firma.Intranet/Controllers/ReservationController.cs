using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.InkML;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Intranet.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IGuestService _guestService;
        private readonly IRoomService _roomService;
        private readonly IExportService _exportService;

        public ReservationController(IReservationService reservationService, IGuestService guestService, IRoomService roomService, IExportService exportService)
        {
            _reservationService = reservationService;
            _guestService = guestService;
            _roomService = roomService;
            _exportService = exportService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _reservationService.GetAllReservations());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["GuestId"] = new SelectList(await _guestService.GetAllGuests(), "Id", "LastName");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Number");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReservationDate,CheckInDate,CheckOutDate,ReservationStatus,GuestId,AdultCount,ChildCount,RoomId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationService.CreateReservation(
                    reservation.CheckInDate,
                    reservation.CheckOutDate,
                    reservation.AdultCount,
                    reservation.ChildCount,
                    reservation.GuestId,
                    reservation.RoomId
                );
                return RedirectToAction(nameof(Index));
            }

            ViewData["GuestId"] = new SelectList(await _guestService.GetAllGuests(), "Id", "LastName",reservation.GuestId);
            ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Number",reservation.RoomId);
            return View(reservation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(await _guestService.GetAllGuests(), "Id", "LastName", reservation.GuestId);
            ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Number", reservation.RoomId);
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReservationDate,CheckInDate,CheckOutDate,ReservationStatus,GuestId,AdultCount,ChildCount,RoomId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationService.UpdateReservation(id,reservation.CheckInDate, reservation.CheckOutDate,reservation.AdultCount,reservation.ChildCount,
                         reservation.GuestId,reservation.RoomId,reservation.ReservationStatus);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_reservationService.ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(await _guestService.GetAllGuests(), "Id", "LastName");
            ViewData["RoomId"] = new SelectList(await _roomService.GetAllRooms(), "Id", "Number");
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationService.GetReservationById(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reservationService.DeleteReservation(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ExportReservationsToExcel()
        {

            var excelFile = await _exportService.ExportAllReservations();

            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Raport rezerwacji z dnia - {DateTime.Today:dd.MM.yyyy}.xlsx");

        }
    }
}
