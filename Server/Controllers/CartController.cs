using System.Security.Claims;
using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue("userId")!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetCart()
        {
            var userId = GetUserId();
            return await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToListAsync();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = GetUserId();
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userId = GetUserId();
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (cartItem == null) return NotFound();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
