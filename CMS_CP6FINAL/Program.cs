using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository; // Add namespace for repositories
using Microsoft.EntityFrameworkCore;

namespace CMS_CP6FINAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Configure JSON options
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use PascalCase for property names
                    options.JsonSerializerOptions.ReferenceHandler =
                        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // Handle cyclic references
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; // Ignore null values
                    options.JsonSerializerOptions.WriteIndented = true; // Format JSON for better readability
                });

            // Configure Entity Framework Core with SQL Server
            builder.Services.AddDbContext<CmsCamp6finalContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));

            // Register custom repository in DI container
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Ensure authentication is configured
            app.UseAuthorization(); // Ensure authorization policies are set up

            app.MapControllers();

            app.Run();
        }
    }
}
