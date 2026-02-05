using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using System.Collections.Generic;

namespace Proyecto_Final_ProgramacionWEB.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDTO> GetAllProducts();
        List<ProductForReadDTO> GetByCategory(int categoryId);
        ProductForReadDTO? GetById(int id);
        ProductForReadDTO AddProduct(ProductCreateUpdateDTO dto);
        void Update(int id, ProductCreateUpdateDTO dto);
        void Delete(int id);
        void UpdateDiscount(int id, int? discount);
        void ToggleHappyHour(int id);
        void ToggleFeatured(int id);
    }
}
