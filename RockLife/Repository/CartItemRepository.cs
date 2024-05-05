using Microsoft.EntityFrameworkCore;
using RockLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockLife.Repository
{
    public class CartItemRepository
    {
        private readonly MyAppContext _context;

        public CartItemRepository(MyAppContext context)
        {
            _context = context;
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem); 
            await _context.SaveChangesAsync(); 
        }

        public async Task<List<(int productId, int quantity)>> GetProductsAndQuantitiesByUserIdAsync(int userId)
        {
            // Здесь мы используем асинхронный LINQ-запрос для получения нужной информации из бд
            var result = await _context.CartItems
                .Where(cartItem => cartItem.UserId == userId) // Фильтруем элементы корзины по ID пользователя
                .Select(cartItem => new { cartItem.ProductId, cartItem.Quantity }) // Выбираем интересующие нас поля
                .ToListAsync(); // Выполняем запрос асинхронно и получаем результат

            // Преобразуем анонимный тип в кортеж для возврата
            return result.Select(item => (item.ProductId, item.Quantity)).ToList();
        }

        public async Task<List< Product>> GetProductDetailsByUserIdAsync(int userId)
        {
            var productDetails = await _context.CartItems
                .Include(cartItem => cartItem.Product)
                .Where(cartItem => cartItem.UserId == userId)
                .Select(cartItem => new Product
                {
                    Type = cartItem.Product.Type,
                    Fabricator = cartItem.Product.Fabricator,
                    Name = cartItem.Product.Name,
                    Price = cartItem.Product.Price
                    // Здесь не включаем Quantity, предполагая, что это детали самого товара
                })
                .ToListAsync();

            return productDetails;
        }

        public async Task<List<CartProductDetails>> GetCartItemQuantitiesByUserIdAsync(int userId)
        {
            return await _context.CartItems
                .Where(cartItem => cartItem.UserId == userId)
                .Select(cartItem => new CartProductDetails
                {
                    Quantity = cartItem.Quantity
                })
                .ToListAsync();
        }

        public async Task<double> CalculateTotalCartValueAsync(int userId)
        {
            using (var context = new MyAppContext())
            {
                var total = await context.CartItems
                    .Where(ci => ci.UserId == userId)
                    .Select(ci => ci.Quantity * ci.Product.Price)
                    .SumAsync();

                return total;
            }
        }

        public async Task RemoveCartItemsByUserIdAsync(int userId)
        {
            using (var context = new MyAppContext()) 
            {
                var cartItems = await context.CartItems
                    .Where(item => item.UserId == userId)
                    .ToListAsync();

                context.CartItems.RemoveRange(cartItems);

                await context.SaveChangesAsync();
            }
        }
    }
}
