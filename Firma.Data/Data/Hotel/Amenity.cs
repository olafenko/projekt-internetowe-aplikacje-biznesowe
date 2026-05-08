using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Amenity
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa nie może być pusta")]
        [Display(Name = "Nazwa udogodnienia")]
        public required string Name { get; set; }

        [Display(Name = "Opis udogodnienia")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [MaxLength(50)]
        [Display(Name="Nazwa klasy ikony bootstrap")]
        public string? Icon{ get; set; }

        public ICollection<Room> Rooms { get; } = new List<Room>();


        [Required]
        public bool IsActive { get; set; } = true;

    }
}
