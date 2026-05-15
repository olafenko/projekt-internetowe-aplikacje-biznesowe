using Firma.Data.Data.Hotel.Enums;
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

        [Display(Name = "Informacje dodatkowe")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Notes { get; set; }

        [ForeignKey("RoomType")]
        [Display(Name = "Typ pokoju")]
        public int RoomTypeId { get; set; }

        [Display(Name = "Typ pokoju")]
        public RoomType? RoomType{ get; set; }

        [Required]
        [Display(Name="Status pokoju")]
        [Column(TypeName = "nvarchar(25)")]
        public RoomStatus RoomStatus { get; set; }

       
        public ICollection<Reservation> Reservations { get; } = new List<Reservation>();

        [Required]
        [Display(Name="Czy aktywny")]
        public bool IsActive { get; set; } = true;



    }
}
