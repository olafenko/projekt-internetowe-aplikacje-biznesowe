using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;

namespace Firma.Intranet.Controllers
{
    public class AdditionalOfferController : Controller
    {
        private readonly IAdditionalOfferService _additionalOfferService;

        public AdditionalOfferController(IAdditionalOfferService additionalOfferService)
        {
            _additionalOfferService = additionalOfferService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _additionalOfferService.GetAllAdditionalOffers());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _additionalOfferService.GetAdditionalOfferById(id.Value);
            if (additionalOffer == null)
            {
                return NotFound();
            }

            return View(additionalOffer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] AdditionalOffer additionalOffer)
        {
            if (ModelState.IsValid)
            {
                await _additionalOfferService.CreateAdditionalOffer(additionalOffer.Name, additionalOffer.Description, additionalOffer.Price);
                return RedirectToAction(nameof(Index));
            }
            return View(additionalOffer);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _additionalOfferService.GetAdditionalOfferById(id.Value);
            if (additionalOffer == null)
            {
                return NotFound();
            }
            return View(additionalOffer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] AdditionalOffer additionalOffer)
        {
            if (id != additionalOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _additionalOfferService.UpdateAdditionalOffer(id, additionalOffer.Name, additionalOffer.Description, additionalOffer.Price);
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_additionalOfferService.AdditionalOfferExists(additionalOffer.Id))
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
            return View(additionalOffer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _additionalOfferService.GetAdditionalOfferById(id.Value);
            if (additionalOffer == null)
            {
                return NotFound();
            }

            return View(additionalOffer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _additionalOfferService.DeleteAdditionalOffer(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
