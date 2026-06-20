using Firma.Data.Data.Hotel;
using Firma.PortalWWW.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IReservationService
    {

        CreateReservationDTO CreateDefaultReservationValues();
        Task<Reservation> CreateReservation(DateTime checkInDate, DateTime checkOutDate,int adultCount, int childCount,int guestId,int roomId);


    }
}
