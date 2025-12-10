using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class PersonsDbContext : DbContext
    {
        public PersonsDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            //Seed to countries
            string countriesJson = System.IO.File.ReadAllText("countries.json");
           List<Country>? countries = System.Text.Json.JsonSerializer.Deserialize<List<Country>>(countriesJson);

            if (countries != null)
            {
                foreach (Country country in countries)
                {
                    modelBuilder.Entity<Country>().HasData(country);
                }
            }


            //Seed to Persons
            string personsJson = System.IO.File.ReadAllText("persons.json");
           List<Person>? persons = System.Text.Json.JsonSerializer.Deserialize<List<Person>>(personsJson);

            if (persons != null)
                foreach (Person person in persons)
                {
                    modelBuilder.Entity<Person>().HasData(person);
                }

            //Fluent API
            // Cari kolom/variable model yang punya nama TIN
            modelBuilder.Entity<Person>().Property(temp => temp.TIN)
                // Ubah jadi nama kolom ini di DB
                .HasColumnName("TaxIdentificationNumber")
                // Ubah ke tipe data ini
                // varchar khusus untuk alphabet dan number. jadi lebih ringan karena tidak perlu karakter khusus
                .HasColumnType("varchar(8)")
                // Berikan nilai default ini jika ada inputan baru.
                .HasDefaultValue("ABC12345");

            // Buat kolom ini sebagai index dan unik
            //modelBuilder.Entity<Person>()
            //    .HasIndex(temp => temp.TIN)
            //    .IsUnique();

            modelBuilder.Entity<Person>().ToTable(t => t.HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8"));

            //Table relations
            //modelBuilder.Entity<Person>(entity =>
            //{
            //    entity.HasOne<Country>(c => c.Country)
            //    .WithMany(p => p.Persons)
            //    .HasForeignKey(p => p.CountryID);
            //});
        }

        // buat eksekusi query stored procedure-nya
        public List<Person> sp_GetAllPerson()
        {
            return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
        }

        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@PersonID", person.PersonID),
                new SqlParameter("@PersonName", person.PersonName),
                new SqlParameter("@Email", person.Email),
                new SqlParameter("@DateOfBirth", person.DateOfBirth),
                new SqlParameter("@Gender", person.Gender),
                new SqlParameter("@CountryID", person.CountryID),
                /* Ini kutambahkan sendiri. Kalau form null, berikan default value. harusnya bisa dari model class sih, gak harus di sini. */
                new SqlParameter("@Address", person.Address ?? "Default address"),
                new SqlParameter("@ReceiveNewsLetters", person.ReceiveNewsLetters),
            };

            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
        }
    }
}
