using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Repo;
using _2._NTBrokersDataBase.Repo.RepositoryUsingEFinMVC.GenericRepository;
using _2._NTBrokersDataBase.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _2._NTBrokersDataBase
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
            var dataBaseLink = "Inmemory";

            if (dataBaseLink == "DefaultConnection")
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<RealEstateEfCoreContext>(d => d.UseSqlServer(connectionString));
            } else
            {
                services.AddDbContext<RealEstateEfCoreContext>(opt => opt.UseInMemoryDatabase("Test"));
            }

            services.AddControllersWithViews();

            //Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ApartmentsRepository>();
            services.AddScoped<BrokersRepository>();
            services.AddScoped<CompaniesRepository>();

            //Services
            services.AddScoped<ViewDataService>();
            services.AddScoped<ApartmentsService>();
            services.AddScoped<CompaniesService>();
            services.AddScoped<BrokersService>();
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
                app.UseExceptionHandler("/Apartments/Error");
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
                    pattern: "{controller=Apartments}/{action=Index}/{id?}");
            });
        }
    }
}
