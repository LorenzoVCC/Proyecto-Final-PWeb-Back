using System.Linq;
using System.Collections.Generic;
using Proyecto_Final_ProgramacionWEB.Data;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using System.Collections.Generic;
using System.Net;

namespace Proyecto_Final_ProgramacionWEB.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _context;

        public CategoryRepository(AppDBContext context)
        {
            _context = context;
        }

        /// /////////////////
        /// /////////////////
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public List<Category> GetByRestaurant(int restaurantid)
        {
            return _context.Categories.Where(c => c.Id_Restaurant == restaurantid).ToList();
        }

        public Category? GetById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id_Category == id);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category, int categoryId)
        {

            if (category.Id_Category == 0)
                category.Id_Category = categoryId;
            else if (category.Id_Category != categoryId)
                return;

            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id_Category == id);
            if (category is null) return;

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
