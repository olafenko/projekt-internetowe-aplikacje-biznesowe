using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class RoomType
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa typu pokoju nie może być pusta")]
        [Display(Name = "Nazwa")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Maksymalna liczba gości nie może być pusta")]
        [Display(Name="Maksymalna liczba gości")]
        public int MaxGuests { get; set; }

        [Required(ErrorMessage = "Liczba łóżek nie może być pusta")]
        [Display(Name = "Liczba łóżek")]
        public int BedCount { get; set; }

        [Required(ErrorMessage = "Powierzchnia nie może być pusta")]
        [Display(Name="Powierzchnia [m2]")]
        public decimal Area { get; set; }


        [Required(ErrorMessage = "Opis rodzaju pokoju nie może być pusty")]
        [Display(Name = "Opis rodzaju pokoju")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Description { get; set; }

        public ICollection<Amenity> Amenities { get;} = new List<Amenity>();

        public ICollection<Room> Rooms { get;} = new List<Room>();

        [Required]
        public bool IsActive { get; set; } = true;



    }
}
