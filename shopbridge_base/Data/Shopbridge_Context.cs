using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Domain.Models;

namespace Shopbridge_base.Data
{
    public class Shopbridge_Context : DbContext
    {
        public Shopbridge_Context (DbContextOptions<Shopbridge_Context> options)
            : base(options)
        {
           // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, CategoryName = "Books" });
            //modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, CategoryName = "Electronics" });
            //modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, CategoryName = "Groceries" });

            //modelBuilder.Entity<Unit>().HasData(new Unit { UnitId = 1, UnitName = "No." });
            //modelBuilder.Entity<Unit>().HasData(new Unit { UnitId = 2, UnitName = "Kg." });
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Product> Product { get; set; }
    }
}
