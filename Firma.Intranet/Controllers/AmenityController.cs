using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.Intranet.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _amenityService.GetAllAmenities());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amenity = await _amenityService.GetAmenityById(id.Value);
            if (amenity == null)
            {
                return NotFound();
            }

            return View(amenity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Icon")] Amenity amenity)
        {
            if (ModelState.IsValid)
            {

                await _amenityService.CreateAmenity(amenity.Name, amenity.Description, amenity.Icon);
                return RedirectToAction(nameof(Index));
            }
            return View(amenity);
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var amenity = await _amenityService.GetAmenityById(id.Value);
            if (amenity == null)
            {
                return NotFound();
            }
            return View(amenity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Icon")] Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _amenityService.UpdateAmenity(id, amenity.Name, amenity.Description, amenity.Icon);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_amenityService.AmenityExists(amenity.Id))
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
            return View(amenity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amenity = await _amenityService.GetAmenityById(id.Value);
            if (amenity == null)
            {
                return NotFound();
            }

            return View(amenity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _amenityService.DeleteAmenity(id.Value);

            return RedirectToAction(nameof(Index));
        }

    }
}
