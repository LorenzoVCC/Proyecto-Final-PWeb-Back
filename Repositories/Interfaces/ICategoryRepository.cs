using Proyecto_Final_ProgramacionWEB;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using System.Collections.Generic;
using Proyecto_Final_ProgramacionWEB.Entities;

namespace Proyecto_Final_ProgramacionWEB.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
        List<Category> GetByRestaurant(int restaurantId);
        Category? GetById(int id);
        void AddCategory(Category category);
        void Update(Category category, int  categoryId);
        void Delete(int id);

    }
}
