using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Hotel
{
    public class RoomTypeService : BaseService, IRoomTypeService
    {
        public RoomTypeService(FirmaContext context) : base(context)
        {
        }

        public async Task<IList<RoomType>> GetRoomTypesByPriceAsc()
        {
            return await _context.RoomType.OrderBy(r => r.BasePrice).ToListAsync();
        }

        public async Task<RoomType?> GetRoomTypeById(int id)
        {
            return await _context.RoomType.Include(r => r.Amenities.Where(a => a.IsActive)).FirstOrDefaultAsync(r => r.Id == id);
        }

        public IList<RoomType> GetAvailableRoomTypes(IList<Room> rooms)
        {
            return rooms.Select(r => r.RoomType).DistinctBy(rt => rt.Id).ToList();
        }

        public async Task<IList<RoomType>> GetAllRoomTypes()
        {
            return await _context.RoomType.Include(r => r.Amenities.Where(a => a.IsActive)).ToListAsync();
        }

        public async Task CreateRoomType(string name, decimal basePrice, string photoUrl, int maxGuests, int bedCount, string description, int[]? selectedAmenities )
        {
            var roomType = new RoomType
            {
                Name = name,
                BasePrice = basePrice,
                PhotoUrl = photoUrl,
                MaxGuests = maxGuests,
                BedCount = bedCount,
                Description = description,
            };

            if (selectedAmenities != null && selectedAmenities.Any())
            {
                roomType.Amenities = await _context.Amenity.Where(a => selectedAmenities.Contains(a.Id)).ToListAsync();
            }

            _context.RoomType.Add(roomType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomType(int id, string name, decimal basePrice, string photoUrl, int maxGuests, int bedCount, string description,int[]? selectedAmenities)
        {
            var roomType = await _context.RoomType.Include(rt=>rt.Amenities).FirstOrDefaultAsync(rt => rt.IsActive && rt.Id == id);

            if (roomType != null)
            {
                roomType.Name = name;
                roomType.BasePrice = basePrice;
                roomType.PhotoUrl = photoUrl;
                roomType.MaxGuests = maxGuests;
                roomType.BedCount = bedCount;
                roomType.Description = description;

                if (selectedAmenities != null && selectedAmenities.Any())
                {
                    roomType.Amenities = await _context.Amenity.Where(a => selectedAmenities.Contains(a.Id)).ToListAsync();
                }

                _context.Update(roomType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRoomType(int id)
        {
            var roomType = await _context.RoomType.FirstOrDefaultAsync(rt => rt.IsActive && rt.Id == id);

            if (roomType != null)
            {
                roomType.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public bool RoomTypeExists(int id)
        {
            return _context.RoomType.Any(rt => rt.IsActive && rt.Id == id);
        }
    }
}
