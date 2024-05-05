using Microsoft.EntityFrameworkCore;
using RockLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockLife.Repository
{
    public class ProductRepository
    {
        private readonly MyAppContext _context;
        public ProductRepository(MyAppContext context) 
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Получение продукта по ID
        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<string?> GetProductFabricatorByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Fabricator)
                .FirstOrDefaultAsync();
        }

        // Получение типа продукта по идентификатору продукта
        public async Task<string?> GetProductTypeByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Type)
                .FirstOrDefaultAsync();
        }

        // Получение имени продукта по идентификатору продукта
        public async Task<string> GetProductNameByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Name)
                .FirstOrDefaultAsync() ?? string.Empty;
        }

        // Получение цены продукта по идентификатору продукта
        public async Task<double?> GetProductPriceByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<string?> GetProductColorByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Color)
                .FirstOrDefaultAsync();
        }

        // Получение описания продукта по идентификатору продукта
        public async Task<string?> GetProductDescriptionByIdAsync(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.Description)
                .FirstOrDefaultAsync();
        }
    }
}
