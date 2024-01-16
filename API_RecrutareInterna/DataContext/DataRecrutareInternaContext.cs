using Microsoft.EntityFrameworkCore;
using API_RecrutateInterna.Models;
using API_RecrutareInterna.Models;
using API_RecrutareInterna.Models.Authentication;



namespace API_RecrutareInterna.DataContext
{
    public class DataRecrutareInternaContext : DbContext { 
        public DataRecrutareInternaContext(DbContextOptions<DataRecrutareInternaContext> options) : base(options) { }

        public DbSet<JobsItem> Jobs { get; set; }

       public DbSet<EchipeItem> Echipe { get; set; }

       public DbSet<BeneficiiItem> Beneficii { get; set; }

       public DbSet<MembriiItem> Membrii { get; set; }

      public DbSet<ProcesInterviuItem> Proces_Interviu { get; set; }

        public DbSet<ProiectItem> Proiecte { get; set; }

    }
}
