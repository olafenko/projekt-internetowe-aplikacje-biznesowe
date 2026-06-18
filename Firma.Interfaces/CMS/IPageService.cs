using Firma.Data.Data.CMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.CMS
{
    public interface IPageService
    {

        Task<Page?> GetPageById(int? id);
        Task<IList<Page>> GetAllPagesByPositionAsc();

    }
}
