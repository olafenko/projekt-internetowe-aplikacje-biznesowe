using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Room
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer pokoju nie może być pusty.")]
        [Display(Name = "Numer pokoju")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Piętro nie może być puste.")]
        [Display(Name = "Piętro")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Opis pokoju nie może być pusty.")]
        [Display(Name = "Opis pokoju")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Cena nie może być pusta.")]
        [Display(Name = "Cena za 1 noc")]
        [Column(TypeName = "money")]
        public decimal PricePerNight{ get; set; }

        [Required(ErrorMessage = "Zdjęcie pokoju nie może być puste.")]
        [Display(Name = "Wybierz zdjęcie pokoju")]
        public required string PhotoUrl{ get; set; }

        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Typ pokoju")]
        public RoomType? RoomType{ get; set; }

        public ICollection<Reservation> Reservations { get; } = new List<Reservation>();

        [Required]
        public bool IsActive { get; set; } = true;



    }
}
