using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Repositories.Implementations;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Proyecto_Final_ProgramacionWEB.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public CategoryService(ICategoryRepository categoryRepository, IRestaurantRepository restaurantRepository)
        {
            _categoryRepository = categoryRepository;
            _restaurantRepository = restaurantRepository;
        }

        /// <summary>
        /// ////////////////////////
        /// </summary>
        /// <returns></returns>

        public List<CategoryForReadDTO> GetAllCategories()
        {
            var categories = _categoryRepository.GetAllCategories();
            return categories.Select(MapToReadDTO).ToList();
        }

        public List<CategoryForReadDTO> GetByRestaurant(int restaurantId)
        {
            var categories = _categoryRepository.GetByRestaurant(restaurantId);
            return categories.Select(MapToReadDTO).ToList();
        }

        public CategoryForReadDTO? GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            return category is null ? null : MapToReadDTO(category);
        }

        public CategoryForReadDTO AddCategory(CategoryCreateUpdateDTO dto)
        {
            var restaurant = _restaurantRepository.GetById(dto.Id_Restaurant);
            if (restaurant is null)
            {
                throw new ArgumentException("El restaurante indicado no existe.");
            }

            var category = new Category
            {
                Name = dto.Name,
                Id_Restaurant = dto.Id_Restaurant
            };

            _categoryRepository.AddCategory(category);
            return MapToReadDTO(category);
        }

        public void Update(int id, CategoryCreateUpdateDTO dto)
        {
            var restaurant = _restaurantRepository.GetById(dto.Id_Restaurant);
            if (restaurant is null)
            {
                throw new ArgumentException("El restaurante indicado no existe.");
            }

            var category = _categoryRepository.GetById(id);
            if (category is null) 
            { 
                throw new ArgumentException("La categoría indicada no existe."); 
            }

            if (dto.Name is not null && dto.Name != "string")
            category.Name = dto.Name;

            category.Id_Restaurant = dto.Id_Restaurant;

            _categoryRepository.Update(category, id);
        }

        public void Delete(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category is null)
            {
                throw new ArgumentException("La categoria indicada no existe.");
            }

            _categoryRepository.Delete(id);
        }

        private static CategoryForReadDTO MapToReadDTO(Category c)
        {
            return new CategoryForReadDTO
            {
                Id_Category = c.Id_Category,
                Name = c.Name,
                Id_Restaurant = c.Id_Restaurant
            };
        }
    }
}
