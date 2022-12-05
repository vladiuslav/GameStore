using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Runtime.CompilerServices;
using WebApi;

namespace WEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        private string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddScoped<IGameService,GameService>();
            services.AddScoped<IGanreService, GanreService>();
            services.AddScoped<ISearchFilterService, SearchFilterService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddControllers();
            services.AddCors();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseHttpsRedirection();  
            app.UseCors(x => x
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowAnyOrigin());
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
