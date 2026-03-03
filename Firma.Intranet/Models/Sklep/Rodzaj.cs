using System.ComponentModel.DataAnnotations;

namespace Firma.Intranet.Models.Sklep
{
    public class Rodzaj
    {

        [Key]
        public int IdRodzaju { get; set; }

        [Required(ErrorMessage = "Nazwa rodzaju nie może być pusta.")]
        [MaxLength(20,ErrorMessage = "Maksymalna długość nazwy rodzaju to 20 znaków.")]
        public required string Nazwa { get; set; }

        public string Opis { get; set; } = string.Empty;

        //to jest powiązanie tabel, rodzaj ma kolekcje towarów danego rodzaju
        public ICollection<Towar> Towar { get;} = new List<Towar>();

    }
}
