using System.Collections.Generic;
using Proyecto_Final_ProgramacionWEB.Entities;

namespace Proyecto_Final_ProgramacionWEB.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        List<Product> GetByCategory(int id);
        Product? GetById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product, int id);
        void Delete(int id);
    }
}
