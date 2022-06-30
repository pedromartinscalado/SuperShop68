using Microsoft.EntityFrameworkCore;
using SuperShop68.Data.Entities;

namespace SuperShop68.Data
{
    public class DataContext : DbContext
    {

        // propriedade que fica ligada ao products
        public DbSet<Product> Products { get; set; }



        // injeto a minha própria DataContext que criei
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
