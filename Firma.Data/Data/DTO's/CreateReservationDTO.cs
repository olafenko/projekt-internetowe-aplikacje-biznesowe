using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.DTO_s
{
    public class CreateReservationDTO
    {
        [Required(ErrorMessage = "Data zameldowania jest wymagana")]
        [Display(Name = "Data zameldowania")]
        public DateTime CheckInDate { get; set; }
        [Required(ErrorMessage = "Data wymeldowania jest wymagana")]
        [Display(Name = "Data wymeldowania")]
        public DateTime CheckOutDate { get; set; }
        [Required]
        [Display(Name = "Liczba dorosłych")]
        public int AdultCount { get; set; }
        [Required]
        [Display(Name = "Liczba dzieci")]
        public int ChildCount { get; set; }
        public int RoomId { get; set; }
        public CreateGuestRequest Guest { get; set; }

    }
}
