using System.ComponentModel.DataAnnotations;

namespace Firma.PortalWWW.DTO_s
{
    public class CreateReservationDTO
    {
        [Required(ErrorMessage = "Data zameldowania jest wymagana")]
        public DateTime CheckInDate { get; set; }
        [Required(ErrorMessage = "Data wymeldowania jest wymagana")]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public int AdultCount { get; set; }
        [Required]
        public int ChildCount { get; set; }
        public int RoomId { get; set; }
        public CreateGuestRequest Guest { get; set; }

    }
}
