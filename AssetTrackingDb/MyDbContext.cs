using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDb
{
    internal class MyDbContext : DbContext
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=MyAssets;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Asset> Assets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We tell the app to use the connectionstring.
            optionsBuilder.UseSqlServer(connectionString);
        }


        protected override void OnModelCreating(ModelBuilder ModelBuilder)
        {
           
        }
    }
}
