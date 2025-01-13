using CMS_CP6FINAL.Model;

using CMS_CP6FINAL.Repository;
using CMS_CP6FINAL.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CMS_CP6FINAL.Repositories;


namespace CMS_CP6FINAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            //CORS -ENABLE

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();

                });
            });

            

            //Register a JWT authentication schema
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"]))
                };
            });



            //3-json format
            builder.Services.AddControllersWithViews()
             .AddJsonOptions(
             options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = null;
                 options.JsonSerializerOptions.ReferenceHandler =
                 System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                 options.JsonSerializerOptions.DefaultIgnoreCondition =
                 System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                 options.JsonSerializerOptions.WriteIndented = true;
             });
            //connection string as Middleware

            // Add services to the container.
          



            builder.Services.AddDbContext<CmsCamp6finalContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("PropelAug24Connection")));


            builder.Services.AddScoped<IReceptionistRepository , ReceptionistRepository >();


            //Doctor
            builder.Services.AddScoped<IViewPatientAppoinmentRepository, ViewPatientAppoinmentRepository>();
            builder.Services.AddScoped<IPatientHistoryDoctorRepository, PatientHistoryDoctorRepository>();
             builder.Services.AddScoped<IDoctorStartConsultationRepository, DoctorStartConsultationRepository>();
            builder.Services.AddScoped<IDoctorLabTestRepository, DoctorLabTestRepository>();

builder.Services.AddScoped<IStaffRepository, StaffRepository>();
            builder.Services.AddScoped<IStaffService, StaffService>();

builder.Services.AddScoped<ILoginRepository, LoginRepository>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
   // Register custom repository in DI container
            builder.Services.AddScoped<IMedicineRepository, MedicineRepository>();


            builder.Services.AddScoped<ILabTestRepository, LabTestRepository>();
builder.Services.AddScoped<ILabTestService, LabTestService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //swagger registration

            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            //enable cors

            app.UseCors("AllowAllOrigin");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthentication();
            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
