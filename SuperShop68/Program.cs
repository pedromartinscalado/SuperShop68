using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShop68.Data;

namespace SuperShop68
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // constroi um host mas n�o arranca e guarda na variavel host
            // o host pode ser trabalho em qualquer sistema operativo
             var host = CreateHostBuilder(args).Build();
            // se n�o existir a base de dados , ele vai ter que criar as tabelas etc
            // e depois vai popular a tabela com dados
            // corre o seeding
            RunSeeding(host);
            // depois corre o host quando tudo estiver feito
            host.Run();
        }


        // O design pattern cria a si pr�prio
        // Refactory
        private static void RunSeeding(IHost host)
        {
            // cria um servi�o
            // vai buscar um servi�o
            // que vai construir
            // n�o � preciso instanciar mas temos que por um direct�rio
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            // cria o servi�o
            using(var scope = scopeFactory.CreateScope())
            {
                // cria um mecanismo para quando ele instanciar a si pr�prio
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                // cria o seeder e espera
                seeder.SeedAsync().Wait();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
