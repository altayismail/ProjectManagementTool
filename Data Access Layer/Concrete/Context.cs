using Entity_Layer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Concrete
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-CJ0SQ81; database=ProjeYönetimToolDB; integrated security=true; MultipleActiveResultSets=true;");
        }

        public DbSet<Admin>? Admins { get; set; }
        public DbSet<Column>? Columns { get; set; }
        public DbSet<Step>? Steps { get; set; }
        public DbSet<TestCase>? TestCases { get; set; }
        public DbSet<TestSet>? TestSets { get; set; }
        public DbSet<Ticket>? Tickets { get; set; }
        public DbSet<User>? Users { get; set; }
    }
}
