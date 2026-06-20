using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
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
        Task<IList<Reservation>> GetAllReservations();
        Task<Reservation?> GetReservationById(int id);

        Task UpdateReservation(int id, DateTime checkInDate, DateTime checkOutDate, int adultCount, int childCount, int guestId, int roomId, ReservationStatus reservationStatus);

        Task DeleteReservation(int id);

        bool ReservationExists(int id);
    }
}
