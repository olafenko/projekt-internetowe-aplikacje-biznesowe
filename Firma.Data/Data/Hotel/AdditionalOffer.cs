using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class AdditionalOffer
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa oferty")]
        public required string Name { get; set; }

        [Required]
        [Display(Name = "Opis oferty")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Cena oferty")]
        public decimal Price { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        [Required]
        public bool IsActive { get; set; } = true;


    }
}
