using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repositories;
using AutoMapper;
using Entities;
using Serilog;

namespace Api
{
    public class Startup
    {
        const string SINGLE_CLIENT_ORIGIN = "single_client_origin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connString = ConfigurationExtensions.GetConnectionString(this.Configuration, "Default");
            string clientApp = Configuration["ClientAddress"];

            ILogger logger = new LoggerConfiguration()
                .ReadFrom.Configuration(this.Configuration)
                .CreateLogger();

            services.AddSingleton<ILogger>(logger);

            services.AddControllers(cfg =>
            {
                cfg.Filters.Add(new Api.Filters.ValidateModelAttribute());
                cfg.Filters.Add(new Api.Filters.ErrorHandlerAttribute(logger));
            });

            services.AddTransient<ReactiveDb.IDatabase>((svc) =>
            {
                return new ReactiveDb.Database(connString);
            });
            services.AddCors(options =>
            {
                options.AddPolicy(SINGLE_CLIENT_ORIGIN, builder =>
                {
                    builder.WithOrigins(clientApp).
                    AllowAnyHeader().
                    AllowAnyMethod();
                });
            });
            services.AddTransient(typeof(IBaseRepo<Cliente>), typeof(ClientesRepo));
            services.AddTransient(typeof(IBaseRepo<Material>), typeof(MaterialesRepo));
            services.AddTransient(typeof(IBaseRepo<DatosFacturacion>), typeof(DatosFacturacionRepo));
            services.AddTransient(typeof(IBaseRepo<Proveedor>), typeof(ProveedoresRepo));
            services.AddTransient(typeof(IBaseRepo<Cuenta>), typeof(CuentasRepo));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(SINGLE_CLIENT_ORIGIN);

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
