using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Firma.Data.Data;
using Firma.Data.Data.Hotel;
using Firma.Interfaces.Hotel;
using Firma.Services.Hotel;

namespace Firma.Intranet.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IReservationService _reservationService;

        public PaymentController(IPaymentService paymentService, IReservationService reservationService)
        {
            _paymentService = paymentService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _paymentService.GetAllPayments());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentService.GetPaymentById(id.Value);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["Reservation"] = new SelectList(await _reservationService.GetAllReservations(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,ReservationId,Method,Status")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.CreatePayment(payment.Amount, payment.Method, payment.ReservationId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Reservation"] = new SelectList(await _reservationService.GetAllReservations(), "Id", "Id", payment.ReservationId);
            return View(payment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentService.GetPaymentById(id.Value);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["Reservation"] = new SelectList(await _reservationService.GetAllReservations(), "Id", "Id", payment.ReservationId);
            return View(payment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,ReservationId,Method,Status,PaymentDate")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _paymentService.UpdatePayment(id, payment.Amount, payment.Method, payment.ReservationId, payment.Status);
                
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_paymentService.PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(await _reservationService.GetAllReservations(), "Id", "Id", payment.ReservationId);
            return View(payment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _paymentService.GetPaymentById(id.Value);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _paymentService.DeletePayment(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
