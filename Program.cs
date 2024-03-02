var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var startup = new Startup(builder.Configuration);

var services = builder.Services;
startup.ConfigureServices(services);

var app = builder.Build();

// Configure the HTTP request pipeline.
var webHostEnvironment = app.Environment;
startup.Configure(app, webHostEnvironment);

app.Run();
