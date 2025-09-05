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
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.ProductType> ProductTypes { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        public DbSet<Models.Supplier> Suppliers { get; set; }
        public DbSet<Models.Region> Regions { get; set; }



    }
}
