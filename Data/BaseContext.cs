
using Microsoft.EntityFrameworkCore;
using CrudUsers0.Models;

namespace CrudUsers0.Data
{

    public class BaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }


    }

}

