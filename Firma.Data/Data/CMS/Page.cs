using Firma.Data.Data.CMS.Enums;
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
        [Display(Name = "Tytuł odnośnika")]
        public required string LinkTitle { get; set; }

        [Display(Name = "Treść strony")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Content { get; set; }

        [Display(Name = "Krótki opis")]
        [Column(TypeName = "nvarchar(150)")]
        public string? ShortDescription { get; set; }

        [Required]
        [Display(Name = "Pozycja ")]
        public int Position { get; set; }

        [Display(Name ="Url obrazka hero")]
        public string? HeroPhotoUrl { get; set; }

        [Display(Name = "Pozycja w menu")]
        public PageMenuArea? PageMenuArea { get; set; } = null;

        [Display(Name ="Nazwa kontrollera (opcjonalne)")]
        public string? ControllerName { get; set; }

        [Display(Name = "Nazwa akcji (opcjonalne)")]
        public string? ActionName { get; set; }

        [Required]
        [Display(Name = "Czy link ma styl CTA")]
        public bool IsLinkCTA { get; set; } = false;

        [Required]
        [Display(Name ="Czy aktywna")]
        public bool IsActive { get; set; } = true;


    }
}
