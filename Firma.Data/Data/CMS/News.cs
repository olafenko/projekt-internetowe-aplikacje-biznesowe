using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class News
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł strony nie może być pusty")]
        [MaxLength(40, ErrorMessage = "Maksymalna długość tytułu strony wynosi 40 znaków.")]
        [Display(Name = "Tytuł strony")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Tytuł linku nie może być pusty")]
        [MaxLength(20, ErrorMessage = "Maksymalna długość tytułu linku aktualności wynosi 20 znaków.")]
        [Display(Name = "Tytuł odnośnika aktualności")]
        public required string LinkTitle { get; set; }


        [Required(ErrorMessage = "Treść aktualości nie może być pusta")]
        [Display(Name = "Treść aktualości")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Content { get; set; }

        [Required(ErrorMessage = "Streszczenie treści aktualości nie może być puste")]
        [Display(Name = "Streszczenie treści aktualości")]
        [Column(TypeName = "nvarchar(max)")]
        public required string ContentSummary { get; set; }


        [Required(ErrorMessage = "Obraz aktualności nie może być pusty.")]
        [Display(Name = "Wybierz obraz aktualności.")]
        public required string ImageUrl { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public bool IsActive { get; set; } = true;

        
    }
}
