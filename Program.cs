using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using GameRouletteBackend.Roulette.Application.Internal.CommandServices;
using GameRouletteBackend.Roulette.Application.Internal.QueryServices;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Roulette.Domain.Services;
using GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Repositories;
using GameRouletteBackend.IAM.Application.Internal.CommandServices;
using GameRouletteBackend.IAM.Application.Internal.QueryServices;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.IAM.Domain.Services;
using GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Repositories;
using GameRouletteBackend.Shared.Domain.Repositories;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

var connectionString = !string.IsNullOrEmpty(builder.Configuration.GetConnectionString("DefaultConnection"))
    ? builder.Configuration.GetConnectionString("DefaultConnection")
    : Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

System.Console.Write(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35)))
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Register repositories
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "GameRouletteBackend.API",
            Version = "v1",
            Description = "Game Roulette Backend API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "ACME Studios",
                Email = "adoa2705@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Roulette Bounded Context Injection Configuration
builder.Services.AddScoped<IRouletteGameRepository, RouletteGameRepository>();
builder.Services.AddScoped<IBetRepository, BetRepository>();
builder.Services.AddScoped<IRouletteCommandService, RouletteCommandService>();
builder.Services.AddScoped<IRouletteQueryService, RouletteQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountCommandService, AccountCommandService>();
builder.Services.AddScoped<IAccountQueryService, AccountQueryService>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    //context.Database.EnsureCreated();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Opcional: para que Swagger sea la página raíz
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
