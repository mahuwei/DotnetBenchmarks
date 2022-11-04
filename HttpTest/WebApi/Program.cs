using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using WebApi.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Async(c => c.File("logs/logs.txt", retainedFileCountLimit: 31,
        rollingInterval: RollingInterval.Day,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext} {Message}{NewLine}{Exception}"))
    .WriteTo.Async(c => c.File("logs/errors/errors.txt", LogEventLevel.Error,
        retainedFileCountLimit: 31, rollingInterval: RollingInterval.Day,
        outputTemplate:
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext} {Message}{NewLine}{Exception}"))
    .WriteTo.Async(c =>
        c.Console(outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {SourceContext} {Message}{NewLine}{Exception}"))
    .CreateLogger();

Log.Logger.Information("ϵͳ��ʼ����...");
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions => {
    serverOptions.Limits.MaxConcurrentConnections = 1000;
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 1000;
    serverOptions.DisableStringReuse = true;
});
builder.Services.AddSingleton<ILoggerFactory>(_ => new SerilogLoggerFactory(Log.Logger));
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<SingletonService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
try{
    app.Run();
}
catch(Exception ex){
    Log.Error(ex, "���з�������");
}
finally{
    Log.Information("�����˳���");
    Log.CloseAndFlush();
}