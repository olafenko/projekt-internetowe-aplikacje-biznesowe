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
    }
}
