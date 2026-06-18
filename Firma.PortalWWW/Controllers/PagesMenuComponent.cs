
using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class PagesMenuComponent : ViewComponent
    {

        private readonly FirmaContext _context;

        public PagesMenuComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("PagesMenuComponent", await _context.Page.OrderBy(p => p.Position).ToListAsync());
        }
    }
}
