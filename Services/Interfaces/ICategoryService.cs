using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using System.Collections.Generic;

namespace Proyecto_Final_ProgramacionWEB.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryForReadDTO> GetAllCategories();
        List<CategoryForReadDTO> GetByRestaurant(int restaurantId);
        CategoryForReadDTO? GetById(int id);
        CategoryForReadDTO AddCategory(CategoryCreateUpdateDTO dto);
        void Update(int id, CategoryCreateUpdateDTO dto);
        void Delete(int id);
    }
}
