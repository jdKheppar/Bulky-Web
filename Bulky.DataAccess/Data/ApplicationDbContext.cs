using Bulky.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAcess
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        //We need to create a property with DbSet in order to create a table
        //in the below line Categories is the name we are giving in the sql server
        public DbSet<Category> JCategories { get; set; }


        //Adding data to the table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category() { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category() { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category() { Id = 3, Name = "History", DisplayOrder = 3 });
        }
    }
}
