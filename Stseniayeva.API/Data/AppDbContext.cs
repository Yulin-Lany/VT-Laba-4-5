using Microsoft.EntityFrameworkCore;
using Stseniayeva.Domain.Entities;

namespace Stseniayeva.API.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Moto> Motos { get; set; }
        public DbSet<MotoGroup> Categories { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



    }
}
