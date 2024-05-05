using System.Collections.Generic;
using RockLife.Models;

namespace RockLife.Repositories
{
    public interface ICartItemRepository
    {
        IEnumerable<CartItem> GetAllCartItems();
        void AddCartItem(CartItem cartItem);
        CartItem FindCartItemById(int? id);
        void RemoveCartItem(int? id);
        void UpdateCartItem(CartItem cartItem);
    }
}