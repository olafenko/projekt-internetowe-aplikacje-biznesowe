using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.DTO_s
{
    public class CreateGuestRequest
    {

        [Required(ErrorMessage = "Imie nie moze byc puste")]
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Nazwisko nie moze byc puste")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email nie moze byc pusty")]
        [EmailAddress]
        [Display(Name = "Adres email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nr telefonu nie moze byc pusty")]
        [Display(Name = "Nr kontaktowy")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Kraj pochodzenia nie moze byc pusty")]
        [Display(Name = "Kraj pochodzenia")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Nr dokumentu tozsamosci nie moze byc pusty")]
        [Display(Name = "Nr dokumentu tożsamości")]
        public string IdentityCardNumber { get; set; }

    }
}
