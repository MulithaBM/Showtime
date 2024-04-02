using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Showtime.Application;
using Showtime.Application.Services.Common;
using Showtime.Application.Services.TenantServices;
using Showtime.Core.Interfaces.Repository;
using Showtime.Core.Interfaces.Services.Common;
using Showtime.Core.Interfaces.Services.Tenant;
using Showtime.Core.Interfaces.UnitOfWork;
using Showtime.Infrastructure.Database;
using Showtime.Infrastructure.Repository;
using Showtime.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MediatREntryPoint).Assembly));

builder.Services.AddDbContext<DataContext>(options =>
{
    options.EnableDetailedErrors(false);
    options.EnableSensitiveDataLogging(false);

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Showtime.Infrastructure"));
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Common services
builder.Services.AddSingleton<IAuthService, AuthService>();

// Tenant services
builder.Services.AddTransient<ITenantService, TenantService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration["JwtSettings:SecretKey"])
            ),
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

// CORS configurations
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
