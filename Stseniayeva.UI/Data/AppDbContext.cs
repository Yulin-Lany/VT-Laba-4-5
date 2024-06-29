using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stseniayeva.Domain.Entities;
using System.Collections;


namespace Stseniayeva.UI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        internal readonly IEnumerable MotoGroup;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Moto> Motos { get; set; }
        public DbSet<MotoGroup> MotoGroups { get; set; }
    }
}
