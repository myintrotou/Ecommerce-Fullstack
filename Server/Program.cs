using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
    options.OperationFilter<Swashbuckle.AspNetCore.Filters.SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData(context);
}

app.Run();

void SeedData(AppDbContext context)
{
    context.Database.EnsureCreated();

    if (!context.Categories.Any())
    {
        var electronics = new Category { Name = "Electronics" };
        var fashion = new Category { Name = "Fashion" };
        context.Categories.AddRange(electronics, fashion);
        context.SaveChanges();

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product { Name = "Ultra Pro Headphones", Description = "Noise-cancelling wireless headphones", Price = 299.99m, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=500", Category = electronics, Stock = 50 },
                new Product { Name = "Smart Watch X2", Description = "Advanced fitness tracker and smartwatch", Price = 199.50m, ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=500", Category = electronics, Stock = 30 },
                new Product { Name = "Minimalist Leather Jacket", Description = "Premium vegan leather jacket", Price = 120.00m, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=500", Category = fashion, Stock = 20 },
                new Product { Name = "Canvas Sneakers", Description = "Comfortable everyday wear sneakers", Price = 45.99m, ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=500", Category = fashion, Stock = 100 }
            );
            context.SaveChanges();
        }
    }
}
