using Firma.Data.Data;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Firma.PortalWWW.Controllers
{
    public class RoomController : Controller
    {

        private readonly IRoomTypeService _roomTypeService;
        private readonly IAmenityService _amenityService;

        public RoomController(IRoomTypeService roomTypeService, IAmenityService amenityService)
        {
            _roomTypeService = roomTypeService;
            _amenityService = amenityService;
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

            ViewBag.AmenityModel = _amenityService.GetRoomTypeAmenities(room);
            
            return View(room);
        }
    }
}
