using Microsoft.EntityFrameworkCore;
using PuntoCuatroSQLiteSQLite.Models;

namespace PuntoCuatroSQLiteSQLite.Data
{
    public class LaboratorioContext : DbContext
    {
        public LaboratorioContext(DbContextOptions<LaboratorioContext> options) : base(options) { }

        public DbSet<Muestra> Muestras { get; set; }
        public DbSet<Ensayo> Ensayos { get; set; }
    }
}
