using Firma.Data.Data;
using Microsoft.AspNetCore.Mvc;

namespace Firma.PortalWWW.Controllers
{
    public class HeroSectionComponent : ViewComponent
    {
        private readonly FirmaContext _context;

        public HeroSectionComponent(FirmaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View("HeroSectionComponent");
            }
            var parsedId = int.Parse(id);
            var pageInfo = await _context.Page.FindAsync(parsedId);

            return View("HeroSectionComponent", pageInfo);
        }
    }
}
