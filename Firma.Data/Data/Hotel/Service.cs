using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Service
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa usługi")]
        public required string Name { get; set; }

        [Required]
        [Display(Name = "Opis usługi")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Cena usługi")]
        public decimal Price { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        [Required]
        public bool IsActive { get; set; } = true;


    }
}
