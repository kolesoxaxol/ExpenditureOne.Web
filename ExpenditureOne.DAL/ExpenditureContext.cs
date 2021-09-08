using ExpenditureOne.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ExpenditureOne.DAL
{
    public class ExpenditureContext : DbContext
    {

        public ExpenditureContext(DbContextOptions<ExpenditureContext> options, IExpenditureInitializer initializer) : base(options)
        {
            // uncomment it to turn on db initialize or continue use migration
            // initializer.Initialize(this);

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
            modelBuilder.Entity<ExpenditureCategory>().HasKey(ec => new { ec.ExpenditureId, ec.CategoryId });
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Category).WithMany(c => c.ExpenditureCategory).HasForeignKey(ec => ec.CategoryId);
            modelBuilder.Entity<ExpenditureCategory>().HasOne(ec => ec.Expenditure).WithMany(e => e.ExpenditureCategory).HasForeignKey(ec => ec.ExpenditureId);
            modelBuilder.Entity<Expenditure>().HasMany(p => p.Categoryies).WithMany(b => b.Expendituries);
            
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, CategoryName = "test category", Color = "red" });
        }

    }
}
