
using Firma.Data.Data;
using Firma.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class PagesMenuComponent : ViewComponent
    {

        private readonly IPageService _pageService;

        public PagesMenuComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("PagesMenuComponent", await _pageService.GetAllPagesByPositionAsc());
        }
    }
}
