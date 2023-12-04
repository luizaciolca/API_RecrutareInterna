using Microsoft.EntityFrameworkCore;
using API_RecrutateInterna.Models;


namespace API_RecrutareInterna.DataContext
{
    public class DataRecrutareInternaContext : DbContext
    {
        public DataRecrutareInternaContext(DbContextOptions<DataRecrutareInternaContext> options) : base(options) { }

        public DbSet<JobsItem> Jobs { get; set; }

    }
}
