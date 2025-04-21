
using Hospital_Management_System.Data;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Dependacy injection 
            builder.Services.AddScoped<HMSDBContext>();

            
            //DB connection
            string conString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<HMSDBContext>(opt =>
            opt.UseSqlServer(conString));

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
