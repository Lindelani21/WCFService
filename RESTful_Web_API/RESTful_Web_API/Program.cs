
using RESTful_Web_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RESTful_Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbPath = Path.GetFullPath("./App_Data/");    // To get absolute path according to where the solution is

            var builder = WebApplication.CreateBuilder(args);

            // Dynamically configure connection string
            builder.Configuration["ConnectionStrings:AssistantGenesisDB_ConnectionString"] = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""{dbPath}Database.mdf"";Integrated Security=True;Connect Timeout=30";

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register database context
            builder.Services.AddDbContext<AssistantGenesisDataContext>(opt => 
            // The entire connection string has to be passed in
            opt.UseSqlServer(builder.Configuration["ConnectionStrings:AssistantGenesisDB_ConnectionString"]));
            // opt.UseInMemoryDatabase("Database"));    // For API connection testing

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
