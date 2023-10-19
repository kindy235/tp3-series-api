using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SeriesApi.Models.EntityFramework;
using SeriesApi.Models.Repository;
using System.Buffers;

namespace SeriesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                      options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<SeriesDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("SeriesDbContext")));

            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();
            builder.Services.AddScoped<IDataRepository<Serie>, SerieManager>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}