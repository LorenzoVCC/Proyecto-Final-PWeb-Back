using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Services.Implementations;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Proyecto_Final_ProgramacionWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        private int GetRestaurantIdFromToken()
        {
            var subClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("El token no contiene 'sub'.");

            return int.Parse(subClaim.Value);
        }


        [HttpGet]
        public ActionResult<List<ProductForReadDTO>> GetAllProducts()
        {
            var list = _productService.GetAllProducts();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ProductForReadDTO> GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product is null) return NotFound();
            return Ok(product);
        }

        [HttpGet("by-category/{categoryId:int}")]
        public ActionResult<ProductForReadDTO> GetByCategory(int categoryId)
        {
            var list = _productService.GetByCategory(categoryId);
            return Ok(list);
        }



        [Authorize]
        [HttpPost]
        public ActionResult<ProductForReadDTO> AddProduct([FromBody] ProductCreateUpdateDTO dto)
        {
            try
            {
                int restaurantId = GetRestaurantIdFromToken();

                var category = _categoryService.GetById(dto.Id_Category);
                if (category is null)
                    return BadRequest("La categoría indicada no existe.");

                if (category.Id_Restaurant != restaurantId)
                    return StatusCode(StatusCodes.Status403Forbidden,"No podés crear productos en categorías de otro restaurante.");


                var created = _productService.AddProduct(dto);
                if (created is null) return NotFound(created);

                return CreatedAtAction(nameof(GetById), new { id = created.Id_Product }, created);
            
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ProductCreateUpdateDTO dto)
        {
            try
            {
                var existing = _productService.GetById(id);
                if (existing is null) return NotFound();

                int tokenRestaurantId = GetRestaurantIdFromToken();

                var currentCategory = _categoryService.GetById(existing.Id_Category);
                if (currentCategory is null) return BadRequest("Categoria Inexistente");

                if (currentCategory.Id_Restaurant != tokenRestaurantId) return StatusCode(StatusCodes.Status403Forbidden,"No podés modificar productos de otro restaurante.");

                if (dto.Id_Category != existing.Id_Category)
                {
                    var newCategory = _categoryService.GetById(dto.Id_Category);
                    if (newCategory is null) return BadRequest("La categoría indicada no existe.");

                    if (newCategory.Id_Restaurant != tokenRestaurantId)
                       return StatusCode(StatusCodes.Status403Forbidden,"No podés asignar una categoría que pertenece a otro restaurante.");
                }

                _productService.Update(id, dto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _productService.GetById(id);
            if (existing is null) return NotFound();

            int tokenRestaurantId = GetRestaurantIdFromToken();

            var category = _categoryService.GetById(existing.Id_Category);
            if (category is null) return BadRequest("La categoría del producto no existe.");

            if (category.Id_Restaurant != tokenRestaurantId)
                return StatusCode(StatusCodes.Status403Forbidden,"No podés borrar productos de otro restaurante.");

            _productService.Delete(id);
            return NoContent();
        }
        

        [Authorize]
        [HttpPatch("{id:int}/discount")]
        public IActionResult UpdateDiscount(int id, [FromBody] ProductDiscountDTO dto)
        {
            var existing = _productService.GetById(id);
            if (existing is null) return NotFound();

            int tokenRestaurantId = GetRestaurantIdFromToken();

            var category = _categoryService.GetById(existing.Id_Category);
            if (category is null) return BadRequest("La categoría del producto no existe.");

            if (category.Id_Restaurant != tokenRestaurantId)
                return StatusCode(StatusCodes.Status403Forbidden, "No podés modificar productos de otro restaurante.");

            _productService.UpdateDiscount(id, dto.Discount);
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id:int}/toggle-happyhour")]
        public IActionResult ToggleHappyHour(int id)
        {
            var existing = _productService.GetById(id);
            if (existing is null) return NotFound();

            int tokenRestaurantId = GetRestaurantIdFromToken();

            var category = _categoryService.GetById(existing.Id_Category);
            if (category is null) return BadRequest("La categoría del producto no existe.");

            if (category.Id_Restaurant != tokenRestaurantId)
                return StatusCode(StatusCodes.Status403Forbidden, "No podés modificar productos de otro restaurante.");

            _productService.ToggleHappyHour(id);
            return NoContent();
        }
    }
}