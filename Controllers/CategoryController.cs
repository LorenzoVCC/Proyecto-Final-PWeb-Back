using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System.Collections.Generic;

namespace Proyecto_Final_ProgramacionWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        private int GetRestaurantIdFromToken()
        {
            var subClaim = User.FindFirst(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("El token no contiene 'sub'.");

            return int.Parse(subClaim.Value);
        }

        //////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult<List<CategoryForReadDTO>> GetAllCategories()
        {
            var list = _categoryService.GetAllCategories();
            return Ok(list);
        }

        //////////////////////////////////////////////////////////
        [HttpGet("{id:int}")]
        public ActionResult<CategoryForReadDTO> GetById(int id)
        {
            var category = _categoryService.GetById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }

        //////////////////////////////////////////////////////////
        [HttpGet("by-restaurant/{restaurantId:int}")]
        public ActionResult<List<CategoryForReadDTO>> GetByRestaurant(int restaurantId)
        {
            var list = _categoryService.GetByRestaurant(restaurantId);
            return Ok(list);
        }

        //////////////////////////////////////////////////////////
        [Authorize]
        [HttpPost]
        public ActionResult<CategoryForReadDTO> Create([FromBody] CategoryCreateUpdateDTO dto)
        {
            try
            {
                int restaurantId = GetRestaurantIdFromToken();
                dto.Id_Restaurant = restaurantId;

                var created = _categoryService.AddCategory(dto);
                if (created is null) return NotFound(created);

                return CreatedAtAction(nameof(GetById), new { id = created.Id_Category }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        
        }


        //////////////////////////////////////////////////////////
        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] CategoryCreateUpdateDTO dto)
        {
            try
            {
                var existing = _categoryService.GetById(id);
                if (existing is null) return NotFound();

                int restaurantId = GetRestaurantIdFromToken();
                
                if (existing.Id_Restaurant != restaurantId) return StatusCode(StatusCodes.Status403Forbidden, "No podés modificar categorías de otro restaurante.");

                dto.Id_Restaurant = restaurantId;                                                   

                _categoryService.Update(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //////////////////////////////////////////////////////////
        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _categoryService.GetById(id);
            if (existing is null) return NotFound();

            int restaurantId = GetRestaurantIdFromToken();

            if (existing.Id_Restaurant != restaurantId) return StatusCode(StatusCodes.Status403Forbidden, "No podés borrar categorías de otro restaurante.");

            _categoryService.Delete(id);
            return NoContent();
        }
    }
}
