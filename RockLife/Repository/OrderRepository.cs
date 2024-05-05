using Microsoft.EntityFrameworkCore;
using RockLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockLife.Repository
{
    public class OrderRepository
    {
        private readonly MyAppContext _context;
        public OrderRepository(MyAppContext context) 
        {
            _context = context;
        }

        public async Task<int> GetTotalOrdersByUserIdAsync(int userId)
        {
            var totalOrders = await _context.Orders.CountAsync(order => order.UserId == userId);
            return totalOrders;
        }

        public async Task AddOrderAsync(int userId, double totalPrice, string status = "новый")
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow, 
                Status = status,
                TotalPrice = totalPrice
            };

            using (var context = new MyAppContext())
            {
                await context.Orders.AddAsync(order);

                await context.SaveChangesAsync();
            }
        }

        
    }
}
