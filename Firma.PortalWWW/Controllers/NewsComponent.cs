using Firma.Data.Data;
using Firma.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class NewsComponent : ViewComponent
    {
        private readonly INewsService _newsService;

        public NewsComponent(INewsService newsService)
        {
            _newsService = newsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("NewsComponent", await _newsService.GetCurrentNewsTakeAsync(3));
        }
    }
}
