using Firma.Data.Data;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class RoomController : Controller
    {

        private readonly IRoomTypeService _roomTypeService;

        public RoomController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<IActionResult> Index()
        {

            var rooms = await _roomTypeService.GetRoomTypesByPriceAsc();

            return View(rooms);
        }


        public async Task<IActionResult> Details(int id)
        {

            var room = await _roomTypeService.GetRoomTypeById(id);

            if (room == null) {

                return NotFound();
            }

            if (room.Amenities.Any())
            {
                ViewBag.AmenityModel = room.Amenities.Where(a => a.IsActive);
            } else
            {
                ViewBag.AmenityModel = null;
            }

            

            return View(room);
        }
    }
}
