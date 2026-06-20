using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
using Firma.Interfaces.Hotel;
using Firma.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Services.Hotel
{
    public class PaymentService : BaseService, IPaymentService
    {
        public PaymentService(FirmaContext context) : base(context)
        {
        }

        public async Task CreatePayment(decimal amount, PaymentMethod paymentMethod, int reservationId)
        {
            var payment = new Payment
            {
                Amount = amount,
                Method = paymentMethod,
                ReservationId = reservationId,
            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _context.Payment.FirstOrDefaultAsync(p => p.Status != PaymentStatus.CANCELLED &&  p.Id == id);

            if (payment != null)
            {
                payment.Status = PaymentStatus.CANCELLED;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Payment>> GetAllPayments()
        {
            return await _context.Payment.Where(p => p.Status != PaymentStatus.CANCELLED).ToListAsync();
        }

        public async Task<Payment?> GetPaymentById(int id)
        {
            return await _context.Payment.FirstOrDefaultAsync(p => p.Status != PaymentStatus.CANCELLED && p.Id == id);
        }

        public bool PaymentExists(int id)
        {
            return _context.Payment.Any(p => p.Status != PaymentStatus.CANCELLED && p.Id == id);
        }

        public async Task UpdatePayment(int id, decimal amount, PaymentMethod paymentMethod, int reservationId, PaymentStatus paymentStatus)
        {
            var payment = await _context.Payment.FirstOrDefaultAsync(p => p.Status != PaymentStatus.CANCELLED && p.Id == id);

            if (payment != null)
            {
                payment.Amount = amount;
                payment.Method = paymentMethod;
                payment.ReservationId = reservationId;
                payment.Status = paymentStatus;

                _context.Update(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
