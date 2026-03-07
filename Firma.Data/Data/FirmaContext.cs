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


    }
}
