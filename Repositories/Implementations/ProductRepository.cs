using Proyecto_Final_ProgramacionWEB.Data;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
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
            //existing.Discount = product.Discount;
            existing.HappyHour = product.HappyHour;
            existing.IsFeatured = product.IsFeatured;
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

        public void UpdateDiscount(int id, int? discount)
        {
            var existing = _context.Products.FirstOrDefault(p => p.Id_Product == id);
            if (existing is null) return;

            existing.Discount = discount;
            _context.SaveChanges();
        }

        public void ToggleHappyHour(int id)
        {
            var existing = _context.Products.FirstOrDefault(p => p.Id_Product == id);
            if (existing is null) return;

            existing.HappyHour = !existing.HappyHour;
            _context.SaveChanges();
        }

        public void ToggleFeatured(int id)
        {
            var existing = _context.Products.FirstOrDefault(p => p.Id_Product == id);
            if (existing is null) return;

            existing.IsFeatured = !existing.IsFeatured;
            _context.SaveChanges();
        }

        public List<Product> Search(ProductSearchDTO query)
        {
            var products = _context.Products.AsQueryable();

            if (query.CategoryId.HasValue)
                products = products.Where(p => p.Id_Category == query.CategoryId.Value);

            if (query.HappyHour.HasValue)
                products = products.Where(p => p.HappyHour == query.HappyHour.Value);

            if (query.Featured.HasValue)
                products = products.Where(p => p.IsFeatured == query.Featured.Value);

            if (query.MinPrice.HasValue)
                products = products.Where(p => p.Price >= query.MinPrice.Value);

            if (query.MaxPrice.HasValue)
                products = products.Where(p => p.Price <= query.MaxPrice.Value);

            if (!string.IsNullOrWhiteSpace(query.Q))
            {
                var q = query.Q.Trim().ToLower();

                products = products.Where(p =>
                    p.Name.ToLower().Contains(q) ||
                    (p.Description != null && p.Description.ToLower().Contains(q))
                );
            }

            return products.ToList();
        }
    }
}
