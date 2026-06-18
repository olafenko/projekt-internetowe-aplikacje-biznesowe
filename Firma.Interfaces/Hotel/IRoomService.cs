using Firma.Data.Data.Hotel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IRoomService
    {

        Task<IList<Room>> GetAvailableRooms(DateTime checkIn, DateTime checkOut, int adults, int children);

    }
}
