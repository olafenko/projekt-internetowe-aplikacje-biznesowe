using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    public class Aktualnosc
    {

        [Key]
        public int IdAktualnosci { get; set; }

        [Required(ErrorMessage = "Tytuł linku nie może być pusty")]
        [MaxLength(10, ErrorMessage = "Maksymalna długość tytułu linku wynosi 10 znaków.")]
        [Display(Name = "Tytuł odnośnika do aktualności")]
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł aktualności nie może być pusty")]
        [MaxLength(40, ErrorMessage = "Maksymalna długość tytułu aktualności wynosi 40 znaków.")]
        [Display(Name = "Tytuł aktualności")]
        public required string Tytul { get; set; }

        [Required(ErrorMessage = "Treść aktualności nie może być pusta")]
        [Display(Name = "Treść aktualności")]
        [Column(TypeName = "nvarchar(max)")]
        public required string Tresc { get; set; }

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [Display(Name = "Pozycja wyświetlania aktualności")]
        public int Pozycja { get; set; }
    }
}
