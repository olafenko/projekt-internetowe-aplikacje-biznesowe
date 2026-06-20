using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Interfaces.CMS;

namespace Firma.Intranet.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pageService.GetAllPagesByPositionAsc());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,LinkTitle,Content,Position,PageMenuArea,HeroPhotoUrl,ShortDescription,ControllerName,ActionName,IsLinkCTA")] Page page)
        {
            if (ModelState.IsValid)
            {
                await _pageService.CreateNewPage(
                    page.Title,
                    page.LinkTitle,
                    page.Content,
                    page.ShortDescription,
                    page.Position,
                    page.HeroPhotoUrl,
                    page.PageMenuArea,
                    page.ControllerName,
                    page.ActionName,
                    page.IsLinkCTA
                );

                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LinkTitle,Content,Position,PageMenuArea,HeroPhotoUrl,ShortDescription,ControllerName,ActionName,IsLinkCTA")] Page page)
        {
            if (id != page.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _pageService.UpdatePage(
                        id,
                        page.Title,
                        page.LinkTitle,
                        page.Content,
                        page.ShortDescription,
                        page.Position,
                        page.HeroPhotoUrl,
                        page.PageMenuArea,
                        page.ControllerName,
                        page.ActionName,
                        page.IsLinkCTA
                    );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_pageService.PageExists(page.Id))
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
            return View(page);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _pageService.GetPageById(id.Value);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pageService.DeletePage(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
