using Microsoft.EntityFrameworkCore;
using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Repositories;
using API_RecrutareInterna.Repositories.Interfaces;


namespace API_RecrutareInterna
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var RecrutareInternaConnectionString = builder.Configuration.GetConnectionString("RecrutareInternaConnection");
            builder.Services.AddDbContext<DataRecrutareInternaContext>(options =>
            options.UseSqlServer(RecrutareInternaConnectionString));


            builder.Services.AddTransient<IJobsRepository, JobsRepository>();

            builder.Services.AddTransient<DataRecrutareInternaContext>();

            var app = builder.Build();

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
