using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Microsoft.Extensions.Hosting;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Proyecto_Final_ProgramacionWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        private int GetRestaurantIdFromToken()
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("El token no contiene el claim 'sub'.");

            return int.Parse(subClaim.Value);
        }

        /////////////////////////////
        [HttpGet] 
        public ActionResult<List<RestaurantForReadDTO>> GetAll()
        {
            var list = _restaurantService.GetAllRestaurants();
            return Ok(list);
        }

        /////////////////////////////
        [HttpGet("{id:int}")]
        public ActionResult<RestaurantForReadDTO> GetbyId(int id)
        {
            var restaurant = _restaurantService.GetById(id);
            if (restaurant is null) return NotFound();
            
            return Ok(restaurant);
        }

        //////////////////////////////////

        /////////////////////////////
        ///
        [HttpPost]
        public ActionResult<Restaurant> Create([FromBody] RestaurantForCreateDTO restaurant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                _restaurantService.AddRestaurant(restaurant);
                return Created();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }


        }

        /////////////////////////////
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] RestaurantForUpdateDTO restaurant)
        {
            var existing = _restaurantService.GetById(id);
            if (existing is null) return NotFound();

            int tokenRestaurantId = GetRestaurantIdFromToken();
            if (id != tokenRestaurantId) return Forbid();

            _restaurantService.Update(restaurant, id);
            return NoContent();
        }

        /////////////////////////////
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _restaurantService.GetById(id);
            if (existing is null) return NotFound();

            int tokenRestaurantId = GetRestaurantIdFromToken();
            if (id != tokenRestaurantId) return Forbid();

            _restaurantService.Delete(id);
            return NoContent();
        }
    }
}
