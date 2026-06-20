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
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _newsService.GetAllNews());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,LinkTitle,Content,ContentSummary,ImageUrl,Date,PublishDate")] News news)
        {
            if (ModelState.IsValid)
            {
                await _newsService.CreateNews(news.Title,news.LinkTitle,news.Content,news.ContentSummary,news.ImageUrl,news.PublishDate);
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LinkTitle,Content,ContentSummary,ImageUrl,Date,PublishDate")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _newsService.UpdateNews(id,news.Title, news.LinkTitle, news.Content, news.ContentSummary, news.ImageUrl, news.PublishDate);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_newsService.NewsExists(news.Id))
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
            return View(news);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _newsService.GetNewsById(id.Value);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _newsService.DeleteNews(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
