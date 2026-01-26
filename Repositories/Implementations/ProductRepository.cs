using Proyecto_Final_ProgramacionWEB.Data;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Proyecto_Final_ProgramacionWEB.Entities;
using Microsoft.EntityFrameworkCore;


namespace Proyecto_Final_ProgramacionWEB.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDBContext _context;

        public ProductRepository(AppDBContext context)
        {
            _context = context;
        }
        ///////////////////////////
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        ///////////////////////////
        public List<Product> GetByCategory(int categoryId)
        {
            return _context.Products
                           .Where(p => p.Id_Category == categoryId)
                           .ToList();
        }
        ///////////////////////////
        public Product? GetById(int id)
        {
            return _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id_Product == id);
        }
        ///////////////////////////
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        ///////////////////////////
        public void UpdateProduct(Product product, int id)
        {
            var existing = _context.Products.FirstOrDefault(p => p.Id_Product == id);
            if (existing is null) return;

            existing.Name = product.Name;
            existing.Description = product.Description;
            existing.Price = product.Price;
            existing.Discount = product.Discount;
            existing.URLImage = product.URLImage;
            existing.Id_Category = product.Id_Category;

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id_Product == id);
            if (product is null) return;
            _context.Products.Remove(product);
            _context.SaveChanges();      
        }
    }
}
