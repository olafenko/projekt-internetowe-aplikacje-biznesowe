using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public  interface IRoomTypeService
    {

        Task<IList<RoomType>> GetRoomTypesByPriceAsc();
        Task<RoomType?> GetRoomTypeById(int id);
        IList<RoomType> GetAvailableRoomTypes(IList<Room> rooms);
        Task<IList<RoomType>> GetAllRoomTypes();
        Task CreateRoomType(string name, decimal basePrice, string photoUrl, int maxGuests, int bedCount, string description, int[] amenities);
        Task UpdateRoomType(int id, string name, decimal basePrice, string photoUrl, int maxGuests, int bedCount, string description,int[] amenities);
        Task DeleteRoomType(int id);
        bool RoomTypeExists(int id);
    }
}
