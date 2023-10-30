using Microsoft.EntityFrameworkCore;

namespace Restaurant.Models
{
    //應對後端資料庫
    public class RestaurantDB:DbContext
    {
        public RestaurantDB(DbContextOptions options) : base(options) 
        {
            Console.WriteLine("DbContext物件起來了");
        }
        public DbSet<Menu> Menu { set; get; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
