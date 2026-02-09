using Proyecto_Final_ProgramacionWEB;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Entities;
using System.Collections.Generic;

namespace Proyecto_Final_ProgramacionWEB.Repositories.Implementations
{
    public interface IRestaurantRepository
    {
        List<Restaurant> GetAllRestaurants();
        Restaurant? GetById(int Id);
        Restaurant? GetByEmail(string email);
        void AddRestaurant(Restaurant restaurant);
        void Update(Restaurant restaurant);
        void Delete(int id);
        bool ExistsByEmail(string email);
    }
}
