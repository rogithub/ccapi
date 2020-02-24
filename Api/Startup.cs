using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using AutoMapper;
using Entities;

namespace Api
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
            string connString = ConfigurationExtensions.GetConnectionString(this.Configuration, "Default");
            services.AddControllers(cfg =>
            {
                cfg.Filters.Add(new Api.Filters.ValidateModelAttribute());
            });
            services.AddTransient<ReactiveDb.IDatabase>((svc) =>
            {
                return new ReactiveDb.Database(connString);
            });
            services.AddTransient(typeof(IBaseRepo<Cliente>), typeof(ClientesRepo));
            services.AddTransient(typeof(IBaseRepo<Material>), typeof(MaterialesRepo));
            services.AddTransient(typeof(IBaseRepo<DatosFacturacion>), typeof(DatosFacturacionRepo));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
