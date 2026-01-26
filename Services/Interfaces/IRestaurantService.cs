using System.Collections.Generic;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;

namespace Proyecto_Final_ProgramacionWEB.Services.Interfaces
{
    public interface IRestaurantService
    {
        List<RestaurantForReadDTO> GetAllRestaurants();
        Restaurant? GetById(int Id);
        public Restaurant? GetByEmail(string email);
        void AddRestaurant(RestaurantForCreateDTO restaurant);
        void Update(RestaurantForUpdateDTO restaurant, int RestaurantId);
        void Delete(int id);
        RestaurantForReadDTO? Authenticate(RestaurantLoginDTO loginDto);

    }
}
