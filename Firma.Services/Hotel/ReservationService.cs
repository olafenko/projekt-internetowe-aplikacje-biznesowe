using Azure.Core;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
using Firma.Interfaces.Hotel;
using Firma.PortalWWW.DTO_s;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Hotel
{
    public class ReservationService : BaseService, IReservationService
    {
        public ReservationService(FirmaContext context) : base(context)
        {
        }

        public CreateReservationDTO CreateDefaultReservationValues()
        {
            var defaultReservationValues = new CreateReservationDTO();

            defaultReservationValues.CheckInDate = DateTime.Now;
            defaultReservationValues.CheckOutDate = defaultReservationValues.CheckInDate.AddDays(1);

            return defaultReservationValues;

        }

        public async Task<Reservation> CreateReservation(DateTime checkInDate, DateTime checkOutDate, int adultCount, int childCount, int guestId, int roomId)
        {

            var nights = (int)Math.Ceiling((checkOutDate - checkInDate).TotalDays);
            var roomPricePerNight = await _context.Room.Where(r => r.Id == roomId).Select(r => r.RoomType.BasePrice).FirstOrDefaultAsync();

            if (nights <= 0) nights = 1;

            var reservation = new Reservation
            {
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                AdultCount = adultCount,
                ChildCount = childCount,
                GuestId = guestId,
                RoomId = roomId,
                IsActive = true,
                TotalPrice = nights * roomPricePerNight

            };

            _context.Add(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task DeleteReservation(int id)
        {
            var reservation = await _context.Reservation.FirstOrDefaultAsync(r => r.IsActive && r.Id == id);

            if (reservation != null)
            {
                reservation.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Reservation>> GetAllReservations()
        {
            return await _context.Reservation.Include(r=> r.Room).Include(r=>r.Guest).Where(r => r.IsActive).ToListAsync();
        }

        public async Task<Reservation?> GetReservationById(int id)
        {
            return await _context.Reservation.Include(r => r.Guest).Include(r => r.Room).FirstOrDefaultAsync(r => r.IsActive && r.Id == id);
        }

        public bool ReservationExists(int id)
        {
            return _context.Reservation.Any(r => r.IsActive && r.Id == id);
        }

        public async Task UpdateReservation(int id, DateTime checkInDate, DateTime checkOutDate, int adultCount, int childCount, int guestId, int roomId, ReservationStatus reservationStatus)
        {
            var reservation = await _context.Reservation.FirstOrDefaultAsync(r => r.IsActive && r.Id == id);

            var nights = (int)Math.Ceiling((checkOutDate - checkInDate).TotalDays);
            var roomPricePerNight = await _context.Room.Where(r => r.Id == roomId).Select(r => r.RoomType.BasePrice).FirstOrDefaultAsync();

            if (nights <= 0) nights = 1;

            if (reservation != null)
            {
                reservation.CheckInDate = checkInDate;
                reservation.CheckOutDate = checkOutDate;
                reservation.AdultCount = adultCount;
                reservation.ChildCount = childCount;
                reservation.GuestId = guestId;
                reservation.RoomId = roomId;
                reservation.TotalPrice = nights * roomPricePerNight;
                reservation.ReservationStatus = reservationStatus;

                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }
        }

    }
}
