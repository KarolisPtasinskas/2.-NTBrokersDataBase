using _2._NTBrokersDataBase.Data;
using _2._NTBrokersDataBase.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

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
            //              >>>> Senas SQL connection <<<<<
            //services.AddTransient<SqlConnection>(_ => new SqlConnection(Configuration["ConnectionStrings:DefaultConnection"]));


            //services.AddDbContext<RealEstateEfCoreContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultEfcore"]));


            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<RealEstateEfCoreContext>(d => d.UseSqlServer(connectionString));
            services.AddControllersWithViews();

            services.AddScoped<ViewDataService>();
            services.AddScoped<ApartmentsService>();
            services.AddScoped<CompaniesService>();
            services.AddScoped<BrokersService>();
            services.AddScoped<FilterService>();
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
                app.UseExceptionHandler("/Helper/Error");
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
                    pattern: "{controller=Helper}/{action=Index}/{id?}");
            });
        }
    }
}
