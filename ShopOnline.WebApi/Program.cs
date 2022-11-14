using Microsoft.EntityFrameworkCore;
using ShopOnline.WebApi.Data;
using ShopOnline.WebApi.GenericRepository;
using ShopOnline.WebApi.GenericRepository.IGenericRepository;
using ShopOnline.WebApi.Repositories;
using ShopOnline.WebApi.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

var dbPath = builder.Configuration["DbPath"];


// Add services to the container.

// Connection to db
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite($"Data Source={dbPath}"));

// Add CORS
builder.Services.AddCors();

// Registering Generic Repository
builder.Services.AddScoped(
    typeof(IRepository<>), typeof(EfRepository<>));

// Registering ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// CORS
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => 
        origin is "https://localhost:7051" 
            or "http://localhost:5108")
    .AllowCredentials()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();