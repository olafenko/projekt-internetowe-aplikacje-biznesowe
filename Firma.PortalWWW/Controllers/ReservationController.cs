using Microsoft.AspNetCore.Mvc;

namespace Firma.PortalWWW.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
