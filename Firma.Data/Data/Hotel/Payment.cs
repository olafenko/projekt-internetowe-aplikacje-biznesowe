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




    }
}
