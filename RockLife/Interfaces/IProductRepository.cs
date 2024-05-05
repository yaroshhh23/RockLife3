using System.Collections.Generic;
using RockLife.Models;

namespace RockLife.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void AddProduct(Product product);
        Product FindProductById(int? id);
        void RemoveProduct(int? id);
        void UpdateProduct(Product product);
    }
}