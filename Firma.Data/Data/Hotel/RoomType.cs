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

        [Required(ErrorMessage = "Cena nie może być pusta.")]
        [Display(Name = "Cena za 1 noc")]
        [Column(TypeName = "money")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "URL Zdjęcia pokoju nie może być puste.")]
        [Display(Name = "URL Zdjęcia pokoju")]
        public required string PhotoUrl { get; set; }

        [Required(ErrorMessage = "Maksymalna liczba osób nie może być pusta")]
        [Display(Name= "Maksymalna liczba osób")]
        [Range(1, 10, ErrorMessage = "Pojemność musi wynosić od 1 do 10 osób")]
        public int MaxGuests { get; set; }

        [Required(ErrorMessage = "Liczba łóżek nie może być pusta")]
        [Display(Name = "Liczba łóżek")]
        public int BedCount { get; set; }

        [Required(ErrorMessage = "Opis rodzaju pokoju nie może być pusty")]
        [Display(Name = "Opis rodzaju pokoju")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Description { get; set; } = string.Empty;

        public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
        public ICollection<Room> Rooms { get;} = new List<Room>();

        [Required]
        [Display(Name="Czy aktywny")]
        public bool IsActive { get; set; } = true;



    }
}
