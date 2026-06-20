using DocumentFormat.OpenXml.InkML;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
using Firma.Interfaces.Hotel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Firma.Intranet.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IAmenityService _amenityService;

        public RoomController(IRoomService roomService, IRoomTypeService roomTypeService, IAmenityService amenityService)
        {
            _roomService = roomService;
            _roomTypeService = roomTypeService;
            _amenityService= amenityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roomService.GetAllRooms());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoomById(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["RoomTypeId"] = new SelectList(await _roomTypeService.GetAllRoomTypes(), "Id", "Name");     
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");     
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Floor,Notes,RoomTypeId,RoomStatus")] Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoom(room.Number, room.Floor, room.RoomTypeId, room.RoomStatus, room.Notes);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomTypeId"] = new SelectList(await _roomTypeService.GetAllRoomTypes(), "Id", "Name", room.RoomTypeId);
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");
            return View(room);

        }

 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _roomService.GetRoomById(id.Value);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["RoomTypeId"] = new SelectList(await _roomTypeService.GetAllRoomTypes(), "Id", "Name", room.RoomTypeId);
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Floor,Notes,RoomTypeId,RoomStatus")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomService.UpdateRoom(id, room.Number, room.Floor, room.RoomTypeId, room.RoomStatus, room.Notes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_roomService.RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomTypeId"] = new SelectList(await _roomTypeService.GetAllRoomTypes(), "Id", "Name", room.RoomTypeId);
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");
            return View(room);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var room = await _roomService.GetRoomById(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomService.DeleteRoom(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
