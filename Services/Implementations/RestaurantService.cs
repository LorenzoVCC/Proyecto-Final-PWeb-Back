using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Repositories.Implementations;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto_Final_ProgramacionWEB.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public List<RestaurantForReadDTO> GetAllRestaurants()
        {
            var restaurant = _restaurantRepository.GetAllRestaurants();
            return restaurant.Select(r => new RestaurantForReadDTO
            {
                Id_Restaurant = r.Id_Restaurant,
                Name = r.Name,
                Email = r.Email,
                Description = r.Description,
                ImageURL = r.ImageURL,
                BGImage = r.BGImage,
                Address = r.Address,
                Slug = r.Slug,
                IsActive = r.IsActive,
                CreatedDate = r.CreatedDate,
                //Id_Gastronomy = r.Id_Gastronomy
            }).ToList();
        }
        
        public Restaurant? GetById(int Id)
        {
            return _restaurantRepository.GetById(Id);
        }

        public Restaurant? GetByEmail(string email)
        {
            return _restaurantRepository.GetByEmail(email);
        }
        public void AddRestaurant(RestaurantForCreateDTO dto)
        {
            var restaurant = new Restaurant
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Description = dto.Description,
                ImageURL = dto.ImageURL,
                BGImage = dto.BGImage,
                Address = dto.Address,
                Slug = dto.Slug,
            };

            _restaurantRepository.AddRestaurant(restaurant);
        }

        public void Update(RestaurantForUpdateDTO dto, int id)
        {
            var restaurant = _restaurantRepository.GetById(id);
            if (restaurant is null)
            {
                throw new ArgumentException("El restaurante indicado no existe.");
            }

            if (dto.Name is not null && dto.Name != "string")
                restaurant.Name = dto.Name;

            if (dto.Email is not null && dto.Email != "string")
                restaurant.Email = dto.Email;

            if (dto.Password is not null && dto.Password != "string")
                restaurant.Password = dto.Password;

            if (dto.Description is not null && dto.Description != "string")
                restaurant.Description = dto.Description;

            if (dto.ImageURL is not null && dto.ImageURL != "string")
                restaurant.ImageURL = dto.ImageURL;

            if (dto.BGImage is not null && dto.BGImage != "string")
                restaurant.BGImage = dto.BGImage;

            if (dto.Address is not null && dto.Address != "string")
                restaurant.Address = dto.Address;

            if (dto.Slug is not null && dto.Slug != "string")
                restaurant.Slug = dto.Slug;

            if (dto.IsActive.HasValue)
                restaurant.IsActive = dto.IsActive.Value;
            /*
            if (dto.Id_Gastronomy.HasValue)
                restaurant.Id_Gastronomy = dto.Id_Gastronomy.Value;*/

            _restaurantRepository.Update(restaurant);
        }

        public void Delete(int id)
        {
            _restaurantRepository.Delete(id);
        }

        public RestaurantForReadDTO? Authenticate(RestaurantLoginDTO loginDto)
        {
            var restaurant = _restaurantRepository.GetByEmail(loginDto.Email);

            if (restaurant is null || restaurant.Password != loginDto.Password)
                return null;

            return new RestaurantForReadDTO
            {
                Id_Restaurant = restaurant.Id_Restaurant,
                Name = restaurant.Name,
                Email = restaurant.Email,
                Description = restaurant.Description,
                ImageURL = restaurant.ImageURL,
                BGImage = restaurant.BGImage,
                Address = restaurant.Address,
                Slug = restaurant.Slug,
                IsActive = restaurant.IsActive,
                CreatedDate = restaurant.CreatedDate,
                //Id_Gastronomy = restaurant.Id_Gastronomy
            };
        }
    }
}


