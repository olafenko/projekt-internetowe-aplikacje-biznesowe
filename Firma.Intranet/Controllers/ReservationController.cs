using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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
        private readonly FirmaContext _context;
        private readonly IExportService _exportService;

        public ReservationController(FirmaContext context, IExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }



        // GET: Reservation
        public async Task<IActionResult> Index()
        {
            var firmaContext = _context.Reservation.Include(r => r.Guest).Include(r => r.Room);
            return View(await firmaContext.ToListAsync());
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            ViewData["GuestId"] = new SelectList(_context.Guest, "Id", "LastName");
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "PhotoUrl");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalPrice,ReservationDate,CheckInDate,CheckOutDate,ReservationStatus,GuestId,AdultCount,ChildCount,RoomId,IsActive")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "Id", "LastName", reservation.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Number", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["GuestId"] = new SelectList(_context.Guest, "Id", "LastName", reservation.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Number", reservation.RoomId);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalPrice,ReservationDate,CheckInDate,CheckOutDate,ReservationStatus,GuestId,AdultCount,ChildCount,RoomId,IsActive")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var nights = (int)Math.Ceiling((reservation.CheckOutDate - reservation.CheckInDate).TotalDays);
                    var roomPricePerNight = await _context.Room.Include(r=>r.RoomType).Where(r => r.Id == reservation.RoomId).Select(r => r.RoomType.BasePrice).FirstOrDefaultAsync();
                    reservation.TotalPrice = nights * roomPricePerNight;
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["GuestId"] = new SelectList(_context.Guest, "Id", "LastName", reservation.GuestId);
            ViewData["RoomId"] = new SelectList(_context.Room, "Id", "Number", reservation.RoomId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Guest)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }


        public async Task<IActionResult> ExportReservationsToExcel()
        {

            var excelFile = await _exportService.ExportAllReservations();

            return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Raport rezerwacji z dnia - {DateTime.Today:dd.MM.yyyy}.xlsx");

        }
    }
}
