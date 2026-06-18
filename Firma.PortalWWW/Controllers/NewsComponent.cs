using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class NewsComponent : ViewComponent
    {
        private readonly FirmaContext _context;

        public NewsComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("NewsComponent", await _context.News.OrderByDescending(p => p.PublishDate).Take(3).ToListAsync());
        }
    }
}
