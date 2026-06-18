using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IAmenityService
    {

        IList<Amenity> GetRoomTypeAmenities(RoomType roomType);

    }
}
