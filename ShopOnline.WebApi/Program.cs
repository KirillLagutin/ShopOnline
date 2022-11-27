using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data.Ef.Data;
using ShopOnline.Data.Ef.GenericRepository;
using ShopOnline.Data.Ef.Repositories;
using ShopOnline.Domain.IGenericRepository;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Domain.Services;
using ShopOnline.Models.Dto;

var builder = WebApplication.CreateBuilder(args);

var dbPath = builder.Configuration["DbPath"];

// Add services to the container.

// Logging
builder.Services.AddHttpLogging(options => //настройка
{
    options.LoggingFields = HttpLoggingFields.RequestHeaders
                            | HttpLoggingFields.ResponseHeaders
                            | HttpLoggingFields.RequestBody
                            | HttpLoggingFields.ResponseBody;
});

// Connection to db
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite($"Data Source={dbPath}"));

// Add CORS
builder.Services.AddCors();

// Registering Generic Repository
builder.Services.AddScoped(
    typeof(IRepository<>), typeof(EfRepository<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// Add Controllers
builder.Services.AddControllers();

// PasswordHasher
builder.Services.Configure<PasswordHasherOptions>(options => 
    options.IterationCount = 100_000);
builder.Services.AddSingleton
    <IPasswordHasher<AccountDto>, PasswordHasher<AccountDto>>();

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

// app.Use(async (context, next) =>
// {
//     var userAgent = context.Request.Headers.UserAgent.ToString();
//     if (userAgent.Contains("Edg"))
//     {
//         await next();
//     }
//     else
//     {
//         await context.Response.WriteAsync(
//             "Браузер не поддерживается, используйте Edg");
//     }
// });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();