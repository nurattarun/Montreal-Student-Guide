using Microsoft.EntityFrameworkCore;
using Montreal_Student_Guide.Models;

namespace Montreal_Student_Guide.Data
{
    public class ApplicationDbContext: DbContext
    {

        //creating constructor of class to send optons here that we will receive from dependence injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
