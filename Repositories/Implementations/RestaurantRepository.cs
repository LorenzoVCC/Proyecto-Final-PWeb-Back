using Microsoft.AspNetCore.Identity; //demas
using Microsoft.EntityFrameworkCore; //demas
using Proyecto_Final_ProgramacionWEB.Data;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS; //demas
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Final_ProgramacionWEB.Repositories.Implementations
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly AppDBContext _context;

        public RestaurantRepository(AppDBContext context)
        {
            _context = context;
        }

        /////////////////Métodos//////////////
        public List<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant? GetById(int Id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id_Restaurant == Id);
        }

        public Restaurant? GetByEmail(string email)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Email.ToLower() == email.ToLower());
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();

            /*
            Restaurant restaurant1 = new Restaurant()
            {
                Name = restaurant.Name,
                Email = restaurant.Email,
                Password = restaurant.Password,
                Description = restaurant.Description,
                ImageURL = restaurant.ImageURL,
                BGImage = restaurant.BGImage,
                Address = restaurant.Address,
                Slug = restaurant.Slug,
            };
            _context.Restaurants.Add(restaurant1);
            _context.SaveChanges();*/
        }

        public void Update(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(r => r.Id_Restaurant == id);

            if (restaurant is null) return;

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        public bool ExistsByEmail(string email)
        {
            var mailSubmit = email.Trim().ToLower();
            return _context.Restaurants.Any(r => r.Email.ToLower() == mailSubmit);
        }
    }
}
