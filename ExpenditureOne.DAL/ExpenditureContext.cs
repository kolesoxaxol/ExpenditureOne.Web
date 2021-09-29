using ExpenditureOne.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            modelBuilder.Entity<ExpenditureCategory>().HasKey(x => new { x.CategoryId, x.ExpenditureId });

            modelBuilder.Entity<Expenditure>().HasMany(x => x.Categories).WithMany(x => x.Expenditures)
                        .UsingEntity<ExpenditureCategory>(x => x.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId),
                                                          x => x.HasOne(x => x.Expenditure).WithMany().HasForeignKey(x => x.ExpenditureId));


            base.OnModelCreating(modelBuilder);
        }

    }
}
