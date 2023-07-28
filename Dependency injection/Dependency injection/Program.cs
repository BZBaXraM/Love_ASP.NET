using Dependency_injection.Data;
using Dependency_injection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// builder.Services.AddTransient<IProductRepository, InMemoryProductRepository>();
// builder.Services.Add(new ServiceDescriptor(typeof(IProductRepository), typeof(InMemoryProductRepository),
//     ServiceLifetime.Singleton));
// builder.Services.AddSingleton<IProductRepository>(new InMemoryProductRepository());
builder.Services.AddSingleton<IProductRepository>(_ => new InMemoryProductRepository(true));
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.Run();