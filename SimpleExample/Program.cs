using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SimpleExample.Data;
using SimpleExample.Enums;
using SimpleExample.Infrastructure.Converters;
using SimpleExample.Infrastructure.Interceptors;
using SimpleExample.Profiles;
using SimpleExample.Repositories;
using SimpleExample.Services;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()

    // Console → show everything
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}"
    )
    // File → only EF Core SQL commands
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(logEvent =>
          logEvent.Properties.ContainsKey("SourceContext") &&
            (
                logEvent.Properties["SourceContext"].ToString().Contains("Microsoft.EntityFrameworkCore.Database.Command")
                || logEvent.Properties["SourceContext"].ToString().Contains("AuditInterceptor")
            )
           )
        .WriteTo.File(
            "Logs/sql_log.txt",
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
            rollingInterval: RollingInterval.Day
        )
    ) .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

// Replace default logging with Serilog
builder.Host.UseSerilog();

// Register interceptor
builder.Services.AddScoped<AuditInterceptor>();

// Register DbContext and configure EF Core
builder.Services.AddDbContext<MyDbContext>((serviceProvider, optionsBuilder) =>
{
    var interceptor = serviceProvider.GetRequiredService<AuditInterceptor>();
    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

    optionsBuilder
        .UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionString"))
        .AddInterceptors(interceptor)   
        .UseLoggerFactory(loggerFactory) 
        .EnableSensitiveDataLogging();   // optional: include parameter values
});

// Create MapperConfiguration
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile<OrderProfiles>();
    cfg.AddProfile<CustomerProfile>();
});

// Create IMapper instance
IMapper mapper = mapperConfig.CreateMapper();

// Register it as a singleton
builder.Services.AddSingleton(mapper);


builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<OrderRepository>();
// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new SafeEnumConverter<PaymentMethod>());
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
