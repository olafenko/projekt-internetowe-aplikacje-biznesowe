using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;

namespace Firma.Intranet.Controllers
{
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;
        private readonly IAmenityService _amenityService;

        public RoomTypeController(IRoomTypeService roomTypeService, IAmenityService amenityService)
        {
            _roomTypeService = roomTypeService;
            _amenityService = amenityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _roomTypeService.GetAllRoomTypes());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetRoomTypeById(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BasePrice,MaxGuests,PhotoUrl,BedCount,Description")] RoomType roomType, int[] selectedAmenities)
        {
            if (ModelState.IsValid)
            {

                await _roomTypeService.CreateRoomType(roomType.Name, roomType.BasePrice, roomType.PhotoUrl, roomType.MaxGuests, roomType.BedCount, roomType.Description, selectedAmenities);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name", selectedAmenities);
            return View(roomType);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetRoomTypeById(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }

            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name");
            return View(roomType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BasePrice,MaxGuests,PhotoUrl,BedCount,Description")] RoomType roomType,int[] selectedAmenities)
        {
            if (id != roomType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _roomTypeService.UpdateRoomType(id, roomType.Name, roomType.BasePrice, roomType.PhotoUrl, roomType.MaxGuests, roomType.BedCount, roomType.Description, selectedAmenities);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_roomTypeService.RoomTypeExists(roomType.Id))
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

            ViewData["Amenities"] = new SelectList(await _amenityService.GetAllAmenities(), "Id", "Name", selectedAmenities);
            return View(roomType);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _roomTypeService.GetRoomTypeById(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _roomTypeService.DeleteRoomType(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
