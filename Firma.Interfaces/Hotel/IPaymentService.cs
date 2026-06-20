using Firma.Data.Data.Hotel;
using Firma.Data.Data.Hotel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Interfaces.Hotel
{
    public interface IPaymentService
    {

        Task<IList<Payment>> GetAllPayments();
        Task<Payment?> GetPaymentById(int id);
        Task CreatePayment(decimal amount,PaymentMethod paymentMethod,int reservationId);
        Task UpdatePayment(int id, decimal amount, PaymentMethod paymentMethod, int reservationId, PaymentStatus paymentStatus);
        Task DeletePayment(int id);
        bool PaymentExists(int id);

    }
}
