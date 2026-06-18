using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
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

        public IList<Amenity> GetRoomTypeAmenities(RoomType roomType)
        {
            return roomType.Amenities.Where(a => a.IsActive).ToList();
        }

    }
}
