using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShopOnline.Data.Ef.Data;
using ShopOnline.Data.Ef.GenericRepository;
using ShopOnline.Data.Ef.Repositories;
using ShopOnline.Domain.Entities;
using ShopOnline.Domain.IGenericRepository;
using ShopOnline.Domain.IRepositories;
using ShopOnline.Domain.Services;
using ShopOnline.WebApi.Configurations;
using ShopOnline.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

var dbPath = builder.Configuration["DbPath"];

// Add services to the container.

// Logging
builder.Services.AddHttpLogging(options => //настройка
{
    options.LoggingFields = HttpLoggingFields.RequestBody
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
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// Add Controllers
builder.Services.AddControllers();

// PasswordHasher
builder.Services.Configure<PasswordHasherOptions>(options => 
    options.IterationCount = 100_000);
builder.Services.AddSingleton
    <IPasswordHasher<Account>, PasswordHasher<Account>>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

JwtConfig jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>();
builder.Services.AddSingleton(jwtConfig);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
          
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();


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

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

// app.Use(async (context, next) =>
// {
//     var transitionsPage = context.Request.Path;
//     
//     await next();
// });

app.MapControllers();

app.Run();