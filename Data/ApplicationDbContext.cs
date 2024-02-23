using API_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace API_CRUD.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    Id = 3,
                    Name = "Client3",
                    Address = "Ruisseau",
                    PhoneNumber = "0999999999",
                    Email = "Client3@outlook.fr",
                    Order = "Basket"

                });
        }// if we want to add some record to our database

    }
}
