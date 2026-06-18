using Firma.Data.Data;
using Firma.Data.Data.CMS;
using Firma.Interfaces.CMS;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.CMS
{
    public class NewsService : BaseService, INewsService
    {
        public NewsService(FirmaContext context) : base(context)
        {
        }

        public async Task<IList<News>> GetAllNews()
        {
            return await _context.News.Where(n => n.IsActive).ToListAsync();
        }

        public async Task<IList<News>> GetCurrentNewsTakeAsync(int amount)
        {
            return await _context.News.OrderByDescending(p => p.PublishDate).Take(amount).ToListAsync();
        }

        public async Task<News?> GetNewsById(int newsId)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == newsId);
        }
    }
}
