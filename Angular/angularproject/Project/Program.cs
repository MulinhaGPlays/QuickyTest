using QuickyTest.Domain.Repositories;
using QuickyTest.Domain.Services;
using QuickyTest.Infra.Repositories;
using QuickyTest.Infra.Services.Mock;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IProveGenerator, ProveGeneratorMock>();
builder.Services.AddTransient<IProveGeneratorRepository, ProveGeneratorRepository>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
