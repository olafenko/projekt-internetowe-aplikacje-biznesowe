using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public ICollection<RoomType> RoomTypes { get; } = new List<RoomType>();


        [Required]
        public bool IsActive { get; set; } = true;

    }
}
