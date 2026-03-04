using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Firma.Intranet.Models.CMS;
using Firma.Intranet.Models.Sklep;

namespace Firma.Intranet.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Firma.Intranet.Models.CMS.Aktualnosc> Aktualnosc { get; set; } = default!;
        public DbSet<Firma.Intranet.Models.CMS.Strona> Strona { get; set; } = default!;
        public DbSet<Firma.Intranet.Models.Sklep.Rodzaj> Rodzaj { get; set; } = default!;
        public DbSet<Firma.Intranet.Models.Sklep.Towar> Towar { get; set; } = default!;
    }
}
