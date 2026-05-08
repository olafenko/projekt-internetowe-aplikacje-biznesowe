using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Guest
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Imie nie może być puste")]
        [Display(Name="Imię")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Nazwisko nie może być puste")]
        [Display(Name = "Nazwisko")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "E-mail nie może być pusty")]
        [Display(Name = "Adres e-mail")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy format adresu e-mail")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Kraj nie może być pusty")]
        [Display(Name = "Kraj")]
        public required string Country { get; set; }

        [Required(ErrorMessage = "Numer telefonu nie może być pusty")]
        [Display(Name = "Numer telefonu")]
        [Phone]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Numer dokumentu tożsamości nie może być pusty")]
        [Display(Name = "Numer dowodu osobistego")]
        [MaxLength(50, ErrorMessage = "Długość numeru dokumentu tożsamości musi wynosić 50 znaków")]
        public required string IdentityCardNumber { get; set; }

        [Display(Name = "Informacje dodatkowe")]
        [MaxLength(500)]
        public string? Notes { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
