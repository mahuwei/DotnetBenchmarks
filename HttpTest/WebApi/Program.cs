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

Log.Logger.Information("系统开始启动...");
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
    Log.Error(ex, "运行发生错误。");
}
finally{
    Log.Information("程序退出。");
    Log.CloseAndFlush();
}