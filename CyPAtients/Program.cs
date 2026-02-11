
using CyPatients.Service;
using CyPatients.Service.interfaces;

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
            builder.Services.AddScoped<IValidation, Validation> ();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
