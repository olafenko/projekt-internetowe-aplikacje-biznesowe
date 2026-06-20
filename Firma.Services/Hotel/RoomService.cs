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
    public class RoomService : BaseService, IRoomService
    {
        public RoomService(FirmaContext context) : base(context)
        {
        }

        public async Task<IList<Room>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int adults, int children)
        {
            return await _context.Room.Include(r => r.RoomType).Where(r => r.IsActive && r.RoomType.MaxGuests >= (adults + children))
               .Where(r => !r.Reservations.Any(rs => rs.CheckOutDate > checkIn && rs.CheckInDate < checkOut)).ToListAsync();
        }

        public Room? GetFirstAvailableRoomByRoomTypeId(IList<Room> availableRooms, int roomTypeId)
        {
            return availableRooms.FirstOrDefault(r => r.IsActive && r.RoomTypeId == roomTypeId);
        }
    }
}
