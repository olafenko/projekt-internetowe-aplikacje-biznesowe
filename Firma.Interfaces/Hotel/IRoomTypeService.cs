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
    }
}
