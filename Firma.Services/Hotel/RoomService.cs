using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
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

        public async Task CreateRoom(string number, int floor, int roomTypeId, RoomStatus status, string? notes)
        {
            var room = new Room
            {
                Number = number,
                Floor = floor,
                RoomTypeId = roomTypeId,
                RoomStatus = status,
                Notes = notes,
            };

            _context.Room.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _context.Room.FirstOrDefaultAsync(r => r.IsActive && r.Id == id);

            if (room != null)
            {
                room.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Room>> GetAllRooms()
        {
            return await _context.Room.Include(r => r.RoomType).Where(r => r.IsActive).ToListAsync();
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

        public async Task<Room?> GetRoomById(int id)
        {
            return await _context.Room.Include(r => r.RoomType).FirstOrDefaultAsync(r => r.IsActive && r.Id == id);
        }
        

        public bool RoomExists(int id)
        {
            return _context.Room.Any(r => r.IsActive && r.Id == id);
        }

        public async Task UpdateRoom(int id, string number, int floor, int roomTypeId, RoomStatus status, string? notes)
        {
            var room = await _context.Room.FirstOrDefaultAsync(r => r.IsActive && r.Id == id);

            if (room != null)
            {
                room.Number = number;
                room.Floor = floor;
                room.RoomTypeId = roomTypeId;
                room.RoomStatus = status;
                room.Notes = notes;

                _context.Update(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}
