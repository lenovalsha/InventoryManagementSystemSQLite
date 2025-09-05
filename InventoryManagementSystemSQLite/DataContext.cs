using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystemSQLite
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = InventoryManagementSystem.db");
        }

        //we need to define the tables in our database
        //Create the tables
        public DbSet<Models.Category> Category { get; set; }
        public DbSet<Models.ProductType> ProductType { get; set; }
        public DbSet<Models.Product> Product { get; set; }
        public DbSet<Models.Supplier> Supplier { get; set; }
        public DbSet<Models.Region> Region { get; set; }



    }
}
