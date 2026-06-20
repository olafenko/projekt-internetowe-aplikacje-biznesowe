using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IAmenityService
    {

        IList<Amenity> GetRoomTypeAmenities(RoomType roomType);
        Task<IList<Amenity>> GetAllAmenities();
        Task<Amenity?> GetAmenityById(int id);

        Task CreateAmenity(string name, string description,string icon);
        Task UpdateAmenity(int id,string name, string description,string icon);
        Task DeleteAmenity(int id);
        bool AmenityExists(int id);


    }
}
