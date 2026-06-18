using Firma.Data.Data.CMS;
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



    }
}
