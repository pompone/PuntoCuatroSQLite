using Microsoft.EntityFrameworkCore;
using PuntoCuatro.Models;

namespace PuntoCuatro.Data
{
    public class LaboratorioContext : DbContext
    {
        public LaboratorioContext(DbContextOptions<LaboratorioContext> options) : base(options) { }

        public DbSet<Muestra> Muestras { get; set; }
        public DbSet<Ensayo> Ensayos { get; set; }
    }
}
