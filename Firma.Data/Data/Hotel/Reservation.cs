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
        [Display(Name = "Cena do zapłaty")]
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
        public int GuestId { get; set; }

        public Guest? Guest { get; set; }

        [ForeignKey("Room")]
        [Display(Name = "Pokój")]
        public int RoomId { get; set; }

        public Room? Room { get; set; }


        [Display(Name = "Usługi dodatkowe")]
        public ICollection<Service> Services { get; set; } = new List<Service>();

        [Required]
        public bool IsActive { get; set; } = true;


    }
}
