using Microsoft.OpenApi.Models;
using ToDo_Web_APi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1"
        , new OpenApiInfo
        {
            Title = "ToDo",
            Version = "v1",
        });
    setup.IncludeXmlComments(@"bin/Debug/net6.0/ToDo Web APi.xml");
});
builder.Services.AddScoped<IAsyncToDoService, ToDoService>();
builder.Services.AddDbContext<ToDoDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();