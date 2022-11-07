using ApiDockerDemo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cb = new ConfigurationBuilder();
cb.AddJsonFile("appsettings.json", false);
cb.AddJsonFile("appsettings.secrets.json", true);
var configurationRoot = cb.Build();
// Add services to the container.

builder.Configuration.AddConfiguration(configurationRoot);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPooledDbContextFactory<DemoDbContext>(options => {
    var connectionString = configurationRoot.GetConnectionString("Default");
    Console.WriteLine(connectionString);
    options.UseSqlServer(connectionString!);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();