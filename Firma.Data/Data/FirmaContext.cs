using Firma.Data.Data.CMS;
using Firma.Data.Data.Hotel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firma.Data.Data
{

    //to jest klasa która reprezentuje całą bazę danych
    public class FirmaContext : DbContext
    {

        public FirmaContext(DbContextOptions<FirmaContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; } = default!;

        public DbSet<Page> Page { get; set; } = default!;

        public DbSet<Room> Room{ get; set; } = default!;

        public DbSet<RoomType> RoomType { get; set; } = default!;
        public DbSet<Amenity> Amenity { get; set; } = default!;
        public DbSet<Guest> Guest { get; set; } = default!;
        public DbSet<AdditionalOffer> Service { get; set; } = default!;
        public DbSet<Reservation> Reservation { get; set; } = default!;
        public DbSet<Payment> Payment{ get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Room>()
                .Property(r => r.RoomStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Reservation>()
                .Property(r => r.ReservationStatus)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Method)
                .HasConversion<string>();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .HasConversion<string>();


        }



    }
}
