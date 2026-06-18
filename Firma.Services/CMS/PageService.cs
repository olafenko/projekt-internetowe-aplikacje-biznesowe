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
    public class PageService : BaseService, IPageService
    {
        public PageService(FirmaContext context) : base(context)
        {
        }

        public async Task<IList<Page>> GetAllPagesByPositionAsc()
        {
            return await _context.Page.OrderBy(p => p.Position).ToListAsync();
        }

        public async Task<Page?> GetPageById(int? id)
        {
            return await _context.Page.FindAsync(id);
        }
    }
}
