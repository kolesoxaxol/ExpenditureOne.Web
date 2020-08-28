using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ExpenditureOne.DAL
{
    public class ExpenditureContext : DbContext
    {

        public ExpenditureContext(DbContextOptions<ExpenditureContext> options, , IExpenditureInitializer initializer) : base(options)
        {
            initializer.Initialize(this);
        }

        public DbSet<Expenditure> Expenditures { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ExpenditureCategory> ExpenditureCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseUseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenditureCategory>().HasKey(bc => new { bc.ExpenditureId, bc.CategoryId });
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Category).WithMany(ec => ec.Expenditures).HasForeignKey(ec => ec.CategoryId);
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Expenditure).WithMany(ec => ec.Categories).HasForeignKey(ec => ec.ExpenditureId);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, CategoryName = "test category", Color = "red" });
        }

    }
}
