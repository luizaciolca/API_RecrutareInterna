using Microsoft.EntityFrameworkCore;
using API_RecrutareInterna.DataContext;
using API_RecrutareInterna.Repositories;
using API_RecrutareInterna.Repositories.Interfaces;
using API_RecrutateInterna.Repositories;
using API_RecrutateInterna.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using MariaCiolca_API2.Repositories;




namespace API_RecrutareInterna
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
    " Enter 'Bearer' [space] and then your token in the text input below." +
    "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });



                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var RecrutareInternaConnectionString = builder.Configuration.GetConnectionString("RecrutareInternaConnection");
            builder.Services.AddDbContext<DataRecrutareInternaContext>(options =>
            options.UseSqlServer(RecrutareInternaConnectionString));


            builder.Services.AddTransient<IJobsRepository, JobsRepository>();
            builder.Services.AddTransient<IEchipeRepository, EchipeRepository>();
            builder.Services.AddTransient<IBeneficiiRepository, BeneficiiRepository>();
            builder.Services.AddTransient<IProcesInterviuRepository, ProcesInterviuRepository>();
            builder.Services.AddTransient<IProiectRepository, ProiectRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            builder.Services.AddTransient<DataRecrutareInternaContext>();


            builder.Logging.AddLog4Net("log4net.config");

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Domain"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey =
                        new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Secret"]))

                    };
                });
            builder.Services.AddAuthorization();


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

