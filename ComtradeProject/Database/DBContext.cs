using Microsoft.EntityFrameworkCore;
using ComtradeProject.Model;

namespace ComtradeProject.Database
{
    public class DBContext : DbContext
    {
       public DbSet<Agent> Agents { get; set; }
       public DbSet<Person> Persons { get; set; }
       public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }
    }
}
