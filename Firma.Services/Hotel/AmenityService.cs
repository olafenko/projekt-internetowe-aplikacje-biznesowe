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
    public class AmenityService : BaseService, IAmenityService
    {
        public AmenityService(FirmaContext context) : base(context)
        {
        }

        public bool AmenityExists(int id)
        {

            return _context.Amenity.Any(e => e.Id == id);
        }

        public async Task CreateAmenity(string name, string description, string icon)
        {
            var amenity = new Amenity{
                Name = name,
                Description = description,
                Icon = icon,
                IsActive = true
            };

            _context.Amenity.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAmenity(int id)
        {
            
            var amenity = await _context.Amenity.FirstOrDefaultAsync(a => a.IsActive && a.Id == id);

            amenity.IsActive = false;

            await _context.SaveChangesAsync();

        }

        public async Task<IList<Amenity>> GetAllAmenities()
        {
            return await _context.Amenity.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<Amenity?> GetAmenityById(int id)
        {
            return await _context.Amenity.FirstOrDefaultAsync(a=> a.IsActive && a.Id == id);
        }

        public IList<Amenity> GetRoomTypeAmenities(RoomType roomType)
        {
            return roomType.Amenities.Where(a => a.IsActive).ToList();
        }

        public  async Task UpdateAmenity(int id, string name, string description, string icon)
        {
            var amenity = await _context.Amenity.FirstOrDefaultAsync(a => a.IsActive && a.Id == id);

            amenity.Name = name;
            amenity.Description = description;
            amenity.Icon = icon;

            _context.Update(amenity);
            await _context.SaveChangesAsync();
                
        }
    }
}
