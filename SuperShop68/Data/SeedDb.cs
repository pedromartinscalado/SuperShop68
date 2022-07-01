using System.Linq;
using System.Threading.Tasks;
using System;
using SuperShop68.Data.Entities;

namespace SuperShop68.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private Random _random;

        public SeedDb(DataContext context)
        {
           _context = context;
           _random = new Random();
        }

        public async Task SeedAsync()
        {
            // vai ver se a base de dados está criada , se ela não estiver criada , ela cria a base de dados
            // se ela já estiver criada avança.
            await _context.Database.EnsureCreatedAsync();

            // criação dos produtos já feitos
            // se não existerem produtos dentro da base de dados
            // vou criar um método para não fazer um a um
            if(!_context.Products.Any())
            {
                AddProduct("iPhone X");
                AddProduct("Magic Mouse");
                AddProduct("iWatch Series 4");
                AddProduct("iPad Mini S");

                // muita atenção ao guardar na base de dados.
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100)
            });
        }
    }
}
