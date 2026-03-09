using Firma.Data.Data.Hotel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Payment
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Kwota")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }


        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        public Reservation? Reservation { get; set; }

        [Required]
        [Display(Name = "Metoda płatności")]
        [Column(TypeName = "nvarchar(25)")]
        public PaymentMethod Method { get; set; } = PaymentMethod.CARD;

        [Required]
        [Display(Name = "Status płatności")]
        [Column(TypeName = "nvarchar(25)")]
        public PaymentStatus Status { get; set; } = PaymentStatus.PENDING;

        [Required]
        [Column(TypeName = "datetime")]
        [Display(Name = "Data płatności")]
        public DateTime PaymentDate{ get; set; }


    }
}
