using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Web;

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
        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 2;
            options.Password.RequiredLength = 8;

        });
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(GetAllQuery).Assembly);
        });
        builder.Services.AddScoped<IToDoRepositry, ToDoItemRepositry>();
        builder.Services.AddScoped<IAuthRepositry, AuthRepositry>();
        builder.Services.AddScoped<ToDoService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ToDoItemMapping).Assembly);
        builder.Services.AddExceptionHandler<CustomExceptionHandling>();
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
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
        app.UseHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.Run();
    }
}

