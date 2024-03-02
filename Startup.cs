
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services;
using WebApi.Utils;
using System.Text;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddControllers();

        // Add JWT Authentication
        Console.WriteLine($"Issuer: {_configuration["Jwt:Issuer"]}");
        Console.WriteLine($"Audience: {_configuration["Jwt:Audience"]}");

        var key = Utils.GenerateRandomKey(16);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256).Key
            };
        });

        // Other services configuration
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine($"Connection String: {connectionString}");
        services.AddDbContext<ApplicationDbContext>(options => 
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();


    }

    public void Configure( WebApplication app, IWebHostEnvironment webHostEnviroment) {
        if (webHostEnviroment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.UseAuthentication();

    }
}