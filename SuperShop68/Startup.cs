using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperShop68.Data;

namespace SuperShop68
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            // cria o objeto generalizado e deita fora da memória.
            // o objeto desaparece e não pode ser mais usado
            // só corre uma vez
            services.AddTransient<SeedDb>();
            

            // o objeto quando for criado nunca vai ser destruido
            // e vai estar sempre disponivel em memória
            // durante o ciclo de vida todo da aplicação
            // é pouco usado porque ocupa muita memória.

            //services.AddSingleton

            // vai gerar as dependencias
            // qualquer objeto que é criado ou serviço aqui,
            // fica criado e está instanciado
            // quando eu criar outro novo tipo de objeto
            // ele apaga esse anterior e sobrepoe um novo
            // é o mais utilizado
            //services.AddScoped

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
