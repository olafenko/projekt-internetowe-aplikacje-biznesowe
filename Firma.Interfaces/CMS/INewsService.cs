using Firma.Data.Data.CMS;
using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.CMS
{
    public interface INewsService
    {


        Task<IList<News>> GetCurrentNewsTakeAsync(int amount);

        Task<News?> GetNewsById(int newsId);

        Task<IList<News>> GetAllNews();

        Task CreateNews(string title, string linkTitle, string content, string contentSummary, string imageUrl, DateTime publishDate);
        Task UpdateNews(int id, string title, string linkTitle, string content, string contentSummary, string imageUrl, DateTime publishDate);
        bool NewsExists(int id);
        Task DeleteNews(int id);

    }
}
