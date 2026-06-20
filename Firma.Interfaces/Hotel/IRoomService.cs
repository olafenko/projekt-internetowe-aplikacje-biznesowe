using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IRoomService
    {

        Task<IList<Room>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int adults, int children);
        Task<IList<Room>> GetAllRooms();
        Room? GetFirstAvailableRoomByRoomTypeId(IList<Room> availableRooms ,int roomTypeId);
        Task<Room?> GetRoomById(int id);
        Task CreateRoom(string number,int floor, int roomTypeId,RoomStatus status,string? notes);
        Task UpdateRoom(int id, string number, int floor, int roomTypeId, RoomStatus status, string? notes);
        Task DeleteRoom(int id);
        bool RoomExists(int id);

    }
}
