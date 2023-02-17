using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Sockets;

namespace LearnShop
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<product> Products { get; set; } = null!;


    }
}
