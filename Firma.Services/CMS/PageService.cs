using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Data.Data.CMS.Enums;
using Firma.Interfaces.CMS;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.CMS
{
    public class PageService : BaseService, IPageService
    {
        public PageService(FirmaContext context) : base(context)
        {
        }

        public async Task CreateNewPage(string title, string linkTitle, string content, string shortDescription, int position, string? heroPhotoUrl, PageMenuArea? pageMenuArea, string? controllerName, string? actionName, bool isLinkCta)
        {
            var newPage = new Page
            {
                Title = title,
                LinkTitle = linkTitle,
                Content = content,
                ShortDescription = shortDescription,
                Position = position,
                HeroPhotoUrl = heroPhotoUrl,
                PageMenuArea = pageMenuArea,
                ControllerName = controllerName,
                ActionName = actionName,
                IsLinkCTA = isLinkCta
            };

            _context.Page.Add(newPage);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePage(int id)
        {
            var page = await _context.Page.FirstOrDefaultAsync(p => p.IsActive && p.Id == id);

            if (page != null)
            {
                page.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Page>> GetAllPagesByPositionAsc()
        {
            return await _context.Page.Where(p => p.IsActive).OrderBy(p => p.Position).ToListAsync();
        }

        public async Task<Page?> GetPageById(int? id)
        {
            return await _context.Page.FirstOrDefaultAsync(p => p.IsActive && p.Id == id);
        }

        public bool PageExists(int id)
        {
            return _context.Page.Any(p => p.IsActive && p.Id == id);
        }

        public async Task UpdatePage(int id, string title, string linkTitle, string content, string shortDescription, int position, string? heroPhotoUrl, PageMenuArea? pageMenuArea, string? controllerName, string? actionName, bool isLinkCta)
        {
            var page = await _context.Page.FirstOrDefaultAsync(p => p.IsActive && p.Id == id);

            if(page != null)
            {
                page.Title = title;
                page.LinkTitle = linkTitle;
                page.Content = content;
                page.ShortDescription = shortDescription;
                page.Position = position;
                page.HeroPhotoUrl = heroPhotoUrl;
                page.PageMenuArea = pageMenuArea;
                page.ControllerName = controllerName;
                page.ActionName = actionName;
                page.IsLinkCTA = isLinkCta;

                _context.Update(page);
                await _context.SaveChangesAsync();
            }
        }

    }
}
