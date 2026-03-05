using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Sklep
{
    public class Towar
    {

        [Key]
        public int IdTowaru { get; set; }

        [Required(ErrorMessage = "Kod towaru nie może być pusty.")]
        public required string Kod { get; set; }


        [Required(ErrorMessage = "Nazwa towaru nie może być pusta.")]
        public required string Nazwa { get; set; }

        [Required(ErrorMessage = "Cena nie może być pusta.")]
        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [Required(ErrorMessage = "URL zdjęcia nie może być puste.")]
        [Display(Name = "Wybierz zdjęcie")]
        public required string PhotoUrl { get; set; }
        public string Opis { get; set; } = string.Empty;

        [ForeignKey("Rodzaj")]
        public int IdRodzaju { get; set; }

        public Rodzaj? Rodzaj { get; set; }

    }
}
