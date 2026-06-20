using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class GuestController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _guestService.GetAllGuests());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuestById(id.Value);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LastName,Email,Country,PhoneNumber,IdentityCardNumber")] Guest guest)
        {
            if (ModelState.IsValid)
            {
                await _guestService.CreateNewGuest(guest.Name, guest.LastName, guest.Email, guest.PhoneNumber, guest.Country, guest.IdentityCardNumber);
                return RedirectToAction(nameof(Index));
            }
            return View(guest);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuestById(id.Value);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Email,Country,PhoneNumber,IdentityCardNumber,Notes")] Guest guest)
        {
            if (id != guest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _guestService.UpdateGuest(id, guest.Name, guest.LastName, guest.Email, guest.PhoneNumber, guest.Country, guest.IdentityCardNumber, guest.Notes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_guestService.GuestExists(guest.Id))
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
            return View(guest);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guest = await _guestService.GetGuestById(id.Value);

            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _guestService.DeleteGuest(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
