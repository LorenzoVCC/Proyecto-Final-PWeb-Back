using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Proyecto_Final_ProgramacionWEB.Data;
using Proyecto_Final_ProgramacionWEB.Repositories.Implementations;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Proyecto_Final_ProgramacionWEB.Services;
using Proyecto_Final_ProgramacionWEB.Services.Implementations;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System.Text;

namespace Proyecto_Final_ProgramacionWEB;

//TERMINAR ESTO
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDBContext>(dbContextOptions => 
        dbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:RestaurantAPIDBConnectionString"]));

        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IPasswordHasher<Proyecto_Final_ProgramacionWEB.Entities.Restaurant>, PasswordHasher<Proyecto_Final_ProgramacionWEB.Entities.Restaurant>>();


        /////////////////////Swagger/////////////////////

        builder.Services.AddSwaggerGen(setupAction =>
        {
            setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Acá pegar el token generado al loguearse."
            });

            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiBearerAuth"
                        }
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Front", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials(); // si NO usás cookies, podés sacarlo
            });
        });


        builder.Services.AddOpenApi();

        var secret = builder.Configuration["Authentication:SecretForKey"]
        ?? throw new InvalidOperationException("Falta Authentication:SecretForKey en config (UserSecrets/appsettings).");


        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)
)
            };
        }
    );

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("Front");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();     
        app.MapControllers();
        app.Run();

    }
}
