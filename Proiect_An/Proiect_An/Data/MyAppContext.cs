using Microsoft.EntityFrameworkCore;
using Proiect_An.Models;

namespace Proiect_An.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options): base(options)
        {

        }
        public DbSet<Item> Items { get; set; }
    }
}
