using Microsoft.EntityFrameworkCore;
using MalmöTradera.web.Models;

namespace westcoast_cars.web.Data
{
    public class ItemsContext : DbContext
    {        
        public ItemsContext(DbContextOptions options) : base(options) { }
        public DbSet<ItemsModel> Items { get; set; }

    }
}