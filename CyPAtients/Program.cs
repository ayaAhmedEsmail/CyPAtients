
using CyPatients.Hubs;
using CyPatients.Service;
using CyPatients.Service.interfaces;
using Microsoft.Extensions.Options;

namespace CyPAtients
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSqlServer<CyPatients.Models.CyhealthCare_dbContext>
                (builder.Configuration.GetConnectionString("DefaultConnection"));

            builder.Services.AddScoped<IpatientService, PatientService>();
            builder.Services.AddScoped<IRegistrationValidation, RegistrationValidation>();
            builder.Services.AddScoped<IValidationService, ValidationService> ();
            
            builder.Services.AddValidation();
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    policy => policy.WithOrigins("http://localhost:5173")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials());
            });

            var app = builder.Build();

            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<CyPatients.Hubs.PatientHub>("/hubs/patient");


            app.Run();
        }
    }
}
