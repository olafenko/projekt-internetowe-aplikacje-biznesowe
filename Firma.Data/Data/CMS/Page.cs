using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class Page
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł strony nie może być pusty")]
        [MaxLength(40, ErrorMessage = "Maksymalna długość tytułu strony wynosi 40 znaków.")]
        [Display(Name = "Tytuł strony")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Tytuł strony nie może być pusty")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość tytułu strony aktualności wynosi 20 znaków.")]
        [Display(Name = "Tytuł odnośnika aktualności")]
        public required string LinkTitle { get; set; }


        [Required(ErrorMessage = "Treść strony nie może być pusta")]
        [Display(Name = "Treść aktualości")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Content { get; set; }

        [Required]
        [Display(Name = "Pozycja strony")]
        public int Position { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;


    }
}
