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
        [Column(TypeName = "datetime")]
        [Display(Name = "Data płatności")]
        public DateTime PaymentDate{ get; set; }


    }
}
