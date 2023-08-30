using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using ToDo_Web_APi;
using ToDo_Web_APi.Data;
using ToDo_Web_APi.DTOs.Validation;
using ToDo_Web_APi.Hosted_Services;
using ToDo_Web_APi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddDbContext<ToDoDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAsyncToDoService, ToDoService>();

Log.Logger = new LoggerConfiguration()
    //.MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.WithProcessName()
    .Enrich.WithThreadId()
    .Enrich.WithThreadName()
    .WriteTo.Console(outputTemplate: "{Timestamp: yyyy / MM / dd   HH:mm:ss} {Level:w3} " +
                                     "{Message: lj} " +
                                     "{NewLine}" +
                                     "ThreadId: {ThreadId} {NewLine}" +
                                     "ThreadName: {ThreadName}{NewLine}" +
                                     "ProcessName: {ProcessName}" +
                                     "{Exception}" +
                                     "{NewLine}")
    //.WriteTo.File("log.txt",
    //            rollingInterval: RollingInterval.Day,
    //            rollOnFileSizeLimit: true)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHostedService<DatabaseClearJob>();

builder.Services.AddLogging(options => { options.AddJsonConsole(); });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.EnablePersistAuthorization());
}

// app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();