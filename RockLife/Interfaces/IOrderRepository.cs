using System.Collections.Generic;
using RockLife.Models;

namespace RockLife.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);
        Order FindOrderById(int? id);
        void RemoveOrder(int? id);
        void UpdateOrder(Order order);
    }
}