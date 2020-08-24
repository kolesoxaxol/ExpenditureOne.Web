using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ExpenditureOne.DAL
{
    public class ExpenditureContext : DbContext
    {

        public ExpenditureContext(DbContextOptions<ExpenditureContext> options) : base(options)
        {
           // this.Database.Migrate();
        }

        //public ExpenditureContext(DbContextOptions<ExpenditureContext> options) : base(options)
        //{ }
        public DbSet<Expenditure> Expenditures { get; set; }

        public DbSet<Category> Categories { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ExpenditureContext"].ConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Category).WithMany(ec => ec.Expenditures).HasForeignKey(ec => ec.CategoryId);
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Expenditure).WithMany(ec => ec.Categories).HasForeignKey(ec => ec.ExpenditureId);
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, CategoryName = "test category", Color = "red" });
        }

    }
}
