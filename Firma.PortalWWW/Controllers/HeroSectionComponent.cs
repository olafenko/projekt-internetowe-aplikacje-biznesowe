using Firma.Data.Data;
using Firma.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;

namespace Firma.PortalWWW.Controllers
{
    public class HeroSectionComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public HeroSectionComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View("HeroSectionComponent");
            }
            var parsedId = int.Parse(id);
            var pageInfo = await _pageService.GetPageById(parsedId);

            return View("HeroSectionComponent", pageInfo);
        }
    }
}
