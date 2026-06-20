using DocumentFormat.OpenXml.Wordprocessing;
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

        public async Task CreateNews(string title, string linkTitle,string content, string contentSummary,string imageUrl,DateTime publishDate)
        {
            var news = new News
            {
                Title = title,
                LinkTitle = linkTitle,
                Content = content,
                ContentSummary = contentSummary,
                ImageUrl = imageUrl,
                PublishDate = publishDate,
            };

            _context.News.Add(news);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNews(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.IsActive && n.Id == id);

            if(news != null)
            {
                news.IsActive = false;
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IList<News>> GetAllNews()
        {
            return await _context.News.Where(n => n.IsActive).ToListAsync();
        }

        public async Task<IList<News>> GetCurrentNewsTakeAsync(int amount)
        {
            return await _context.News.Where(n => n.IsActive).OrderByDescending(p => p.PublishDate).Take(amount).ToListAsync();
        }

        public async Task<News?> GetNewsById(int newsId)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.IsActive && n.Id == newsId);
        }

        public bool NewsExists(int id)
        {
            return _context.News.Any(n => n.IsActive && n.Id == id);
        }

        public async Task UpdateNews(int id, string title, string linkTitle, string content, string contentSummary, string imageUrl, DateTime publishDate)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.IsActive && n.Id == id);

            if(news != null)
            {
                news.Title = title;
                news.LinkTitle = linkTitle;
                news.Content = content;
                news.ContentSummary = contentSummary;
                news.ImageUrl = imageUrl;
                news.PublishDate = publishDate;

                _context.Update(news);
                await _context.SaveChangesAsync();
            }
          
        }
    }
}
