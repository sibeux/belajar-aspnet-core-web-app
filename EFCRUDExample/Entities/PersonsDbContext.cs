using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Country>().ToTable("Persons");

            //Seed to countries
            string countriesJson = System.IO.File.ReadAllText("countries.json");
           List<Country> countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            foreach (Country country in countries)
            {
                modelBuilder.Entity<Country>().HasData(country);
            }

            //Seed to Persons
            string personsJson = System.IO.File.ReadAllText("countries.json");
           List<Country> persons = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(personsJson);

            foreach (Country person in persons)
            {
                modelBuilder.Entity<Country>().HasData(persons);
            }
       }
    }
}
