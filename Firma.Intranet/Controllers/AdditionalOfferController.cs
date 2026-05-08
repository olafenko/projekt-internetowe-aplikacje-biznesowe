using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;

namespace Firma.Intranet.Controllers
{
    public class AdditionalOfferController : Controller
    {
        private readonly FirmaContext _context;

        public AdditionalOfferController(FirmaContext context)
        {
            _context = context;
        }

        // GET: AdditionalOffer
        public async Task<IActionResult> Index()
        {
            return View(await _context.Service.ToListAsync());
        }

        // GET: AdditionalOffer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalOffer == null)
            {
                return NotFound();
            }

            return View(additionalOffer);
        }

        // GET: AdditionalOffer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdditionalOffer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,IsActive")] AdditionalOffer additionalOffer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additionalOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(additionalOffer);
        }

        // GET: AdditionalOffer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _context.Service.FindAsync(id);
            if (additionalOffer == null)
            {
                return NotFound();
            }
            return View(additionalOffer);
        }

        // POST: AdditionalOffer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,IsActive")] AdditionalOffer additionalOffer)
        {
            if (id != additionalOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalOfferExists(additionalOffer.Id))
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

        // GET: AdditionalOffer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalOffer = await _context.Service
                .FirstOrDefaultAsync(m => m.Id == id);
            if (additionalOffer == null)
            {
                return NotFound();
            }

            return View(additionalOffer);
        }

        // POST: AdditionalOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalOffer = await _context.Service.FindAsync(id);
            if (additionalOffer != null)
            {
                _context.Service.Remove(additionalOffer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalOfferExists(int id)
        {
            return _context.Service.Any(e => e.Id == id);
        }
    }
}
