
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoApp.Application.CQRS.Queries;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Exceptions;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Mapping;
using ToDoApp.Application.Services;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Repositries;

namespace ToDoApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssembly(typeof(GetAllQuery).Assembly);
            });
            builder.Services.AddScoped<IToDoRepositry, ToDoItemRepositry>();
            builder.Services.AddScoped<ToDoService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            TypeAdapterConfig.GlobalSettings.Scan(typeof(ToDoItemMapping).Assembly);
            builder.Services.AddExceptionHandler<CustomExceptionHandling>();
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
            app.UseExceptionHandler(i => { });
            app.Run();
        }
    }
}
