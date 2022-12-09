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
            //ModelBuilder.Entity<Car>().HasData(new Car { Id = 1, Model = "V70", Brand = "Volvo", Year = "2005" });
            //ModelBuilder.Entity<Car>().HasData(new Car { Id = 2, Model = "9000", Brand = "SAAB", Year = "2001" });
            //ModelBuilder.Entity<Car>().HasData(new Car { Id = 3, Model = "540", Brand = "BMW", Year = "2010" });

            //ModelBuilder.Entity<Engine>().HasData(new Engine { Id = 1, Volume = 2, NumberOfPistons = 4, CarId = 1 });
            //ModelBuilder.Entity<Engine>().HasData(new Engine { Id = 2, Volume = 3, NumberOfPistons = 6, CarId = 2 });
            //ModelBuilder.Entity<Engine>().HasData(new Engine { Id = 3, Volume = 4, NumberOfPistons = 8, CarId = 3 });

            //ModelBuilder.Entity<Bus>().HasData(new Bus { Id = 1, Brand = "Volvo", NumberOfSeats = 14 });
            //ModelBuilder.Entity<Bus>().HasData(new Bus { Id = 2, Brand = "Mercedes", NumberOfSeats = 32 });

            //ModelBuilder.Entity<Passenger>().HasData(new Passenger { Id = 1, Name = "Henrik", BusId = 1 });
            //ModelBuilder.Entity<Passenger>().HasData(new Passenger { Id = 2, Name = "Sara", BusId = 1 });
            //ModelBuilder.Entity<Passenger>().HasData(new Passenger { Id = 3, Name = "Johan", BusId = 2 });
            //ModelBuilder.Entity<Passenger>().HasData(new Passenger { Id = 4, Name = "Vilma", BusId = 2 });

            //ModelBuilder.Entity<Book>().HasData(new Book { Id = 1, Title = "James Bond in Italy" });
            //ModelBuilder.Entity<Book>().HasData(new Book { Id = 2, Title = "Football is fun" });

            //ModelBuilder.Entity<Author>().HasData(new Author { Id = 1, Name = "Leo Messi" });
            //ModelBuilder.Entity<Author>().HasData(new Author { Id = 2, Name = "Cristiano Ronaldo" });
            //ModelBuilder.Entity<Author>().HasData(new Author { Id = 3, Name = "Erling Haaland" });
        }
    }
}
