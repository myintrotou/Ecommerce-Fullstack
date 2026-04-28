using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Note: We don't use context.Database.EnsureCreated() here 
            // because we are using Migrations for schema management.

            if (!context.Categories.Any())
            {
                var electronics = new Category { Name = "Electronics" };
                var fashion = new Category { Name = "Fashion" };
                var home = new Category { Name = "Home & Living" };
                var accessories = new Category { Name = "Accessories" };
                
                context.Categories.AddRange(electronics, fashion, home, accessories);
                context.SaveChanges();

                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        // Electronics
                        new Product { Name = "Ultra Pro Headphones", Description = "Noise-cancelling wireless headphones with 40h battery life.", Price = 299.99m, ImageUrl = "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?w=800", Category = electronics, Stock = 50 },
                        new Product { Name = "Smart Watch X2", Description = "Advanced fitness tracker with heart rate monitor and GPS.", Price = 199.50m, ImageUrl = "https://images.unsplash.com/photo-1523275335684-37898b6baf30?w=800", Category = electronics, Stock = 30 },
                        new Product { Name = "Mechanical Keyboard", Description = "RGB backlit mechanical keyboard with blue switches.", Price = 89.99m, ImageUrl = "https://images.unsplash.com/photo-1511467687858-23d96c32e4ae?w=800", Category = electronics, Stock = 15 },
                        
                        // Fashion
                        new Product { Name = "Minimalist Leather Jacket", Description = "Premium vegan leather jacket with slim-fit design.", Price = 120.00m, ImageUrl = "https://images.unsplash.com/photo-1551028719-00167b16eac5?w=800", Category = fashion, Stock = 20 },
                        new Product { Name = "Canvas Sneakers", Description = "Comfortable everyday wear sneakers in multiple colors.", Price = 45.99m, ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?w=800", Category = fashion, Stock = 100 },
                        new Product { Name = "Summer Linen Shirt", Description = "Breathable linen shirt perfect for hot weather.", Price = 35.00m, ImageUrl = "https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=800", Category = fashion, Stock = 40 },

                        // Home & Living
                        new Product { Name = "Ceramic Coffee Set", Description = "Handcrafted ceramic set including 4 mugs and a pot.", Price = 55.00m, ImageUrl = "https://images.unsplash.com/photo-1514228742587-6b1558fbed20?w=800", Category = home, Stock = 12 },
                        new Product { Name = "Modern Table Lamp", Description = "Sleek LED table lamp with adjustable brightness.", Price = 42.50m, ImageUrl = "https://images.unsplash.com/photo-1507473885765-e6ed057f782c?w=800", Category = home, Stock = 25 },
                        new Product { Name = "Wool Throw Blanket", Description = "Soft and warm 100% merino wool blanket.", Price = 75.00m, ImageUrl = "https://images.unsplash.com/photo-1580301762395-21ce84d00bc6?w=800", Category = home, Stock = 18 },

                        // Accessories
                        new Product { Name = "Classic Aviator Sunglasses", Description = "Polarized lenses with a premium gold frame.", Price = 125.00m, ImageUrl = "https://images.unsplash.com/photo-1511499767350-a1590fdb7301?w=800", Category = accessories, Stock = 60 },
                        new Product { Name = "Minimalist Quartz Watch", Description = "Clean dial design with a genuine leather strap.", Price = 150.00m, ImageUrl = "https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=800", Category = accessories, Stock = 10 },
                        new Product { Name = "Leather Travel Wallet", Description = "Slim RFID blocking wallet for your essentials.", Price = 39.99m, ImageUrl = "https://images.unsplash.com/photo-1627123424574-724758594e93?w=800", Category = accessories, Stock = 45 }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
