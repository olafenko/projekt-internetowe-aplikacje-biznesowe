using Firma.Data.Data.Hotel.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Firma.Data.Data.Hotel
{
    public class Reservation
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Cena całkowita")]
        public decimal TotalPrice{ get; set; }

        [Required]
        [Display(Name = "Data złożenia rezerwacji")]
        [Column(TypeName = "datetime")]
        public DateTime ReservationDate { get; set; } = DateTime.Now;


        [Required]
        [Display(Name = "Data zameldowania")]
        [Column(TypeName="datetime")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Data wymeldowania")]
        [Column(TypeName = "datetime")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Display(Name = "Status rezerwacji")]
        [Column(TypeName = "nvarchar(25)")]
        public ReservationStatus ReservationStatus { get; set; } = ReservationStatus.PENDING;

        [ForeignKey("Guest")]
        [Display(Name = "Gość")]
        public int GuestId { get; set; }

        [Display(Name = "Gość")]
        public Guest? Guest { get; set; }

        [Required]
        [Display(Name = "Liczba dorosłych")]
        public int AdultCount { get; set; }

        [Display(Name = "Liczba dzieci")]
        public int ChildCount { get; set; }

        [ForeignKey("Room")]
        [Display(Name = "Pokój")]
        public int RoomId { get; set; }

        [Display(Name = "Pokój")]
        public Room? Room { get; set; }


        [Display(Name = "Usługi dodatkowe")]
        public ICollection<AdditionalOffer> AdditionalOffers { get; set; } = new List<AdditionalOffer>();

        [Required]
        public bool IsActive { get; set; } = true;


    }
}
