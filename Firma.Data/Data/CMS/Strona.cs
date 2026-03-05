using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.CMS
{
    //to jest klasa reprezentująca tabele przechowującą wszystkie strony z ich zawartością
    public class Strona
    {

        [Key] //adnotacja primary key tabeli
        public int IdStrony { get; set; }

        [Required(ErrorMessage = "Tytuł linku nie może być pusty")] // pole wymagane
        [MaxLength(10, ErrorMessage = "Maksymalna długość tytułu linku wynosi 10 znaków.")]
        [Display(Name = "Tytuł odnośnika")] // nazwa pola dla zwykłego użytkownika jak jest różna niż nazwa propa
        public required string LinkTytul { get; set; }

        [Required(ErrorMessage = "Tytuł strony nie może być pusty")]
        [MaxLength(40, ErrorMessage = "Maksymalna długość tytułu strony wynosi 40 znaków.")]
        [Display(Name = "Tytuł strony")]
        public required string Tytul { get; set; }

        [Required(ErrorMessage = "Treść strony nie może być pusta")]
        [Display(Name = "Treść strony")]
        [Column(TypeName = "nvarchar(max)")] //decyduje jakiego typu jest pole w bazie danych
        public required string Tresc { get; set; }

        [Required(ErrorMessage = "Pozycja jest wymagana")]
        [Display(Name = "Pozycja wyświetlania strony")]
        public int Pozycja { get; set; }


    }
}
