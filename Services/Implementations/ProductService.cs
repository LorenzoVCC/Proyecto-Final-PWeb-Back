using Proyecto_Final_ProgramacionWEB.Entities;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Repositories.Implementations;
using Proyecto_Final_ProgramacionWEB.Repositories.Interfaces;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Proyecto_Final_ProgramacionWEB.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public List<ProductForReadDTO> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            return products.Select(MapToReadDTO).ToList();
        }

        public List<ProductForReadDTO> GetByCategory(int categoryId)
        {
            var products = _productRepository.GetByCategory(categoryId);
            return products.Select(MapToReadDTO).ToList();
        }

        public ProductForReadDTO? GetById(int id)
        {
            var product = _productRepository.GetById(id);
            return product is null ? null : MapToReadDTO(product);
        }

        public ProductForReadDTO AddProduct(ProductCreateUpdateDTO dto)
        {
            var category = _categoryRepository.GetById(dto.Id_Category);
            if (category is null)
            {
                throw new ArgumentException("La categoria indicada no existe.");
            }

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Discount = dto.Discount,
                URLImage = dto.URLImage,
                Id_Category = dto.Id_Category
            };
            _productRepository.AddProduct(product);
            return MapToReadDTO(product);
            
        }

        public void Update(int id, ProductCreateUpdateDTO dto)
        {
            var category = _categoryRepository.GetById(dto.Id_Category);
            if (category is null)
            {
                throw new ArgumentException("La categoría indicada no existe.");
            }

            var product = _productRepository.GetById(id);
            if (product is null)
            {
                throw new ArgumentException("El producto indicado no existe.");
            }

            if (dto.Name is not null && dto.Name!= "string")
                product.Name = dto.Name;

            if (dto.Description is not null && dto.Description != "string")
                product.Description = dto.Description;
                        
            if (dto.URLImage is not null && dto.URLImage != "string")
                product.URLImage = dto.URLImage;

            product.Price = dto.Price;
            product.Discount = dto.Discount;

            product.Id_Category = dto.Id_Category;

            _productRepository.UpdateProduct(product, id);
        }

        public void Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product is null)
            {
                throw new ArgumentException("El producto indicado no existe.");
            }
            _productRepository.Delete(id);
        }


        private static ProductForReadDTO MapToReadDTO(Product p)
        {
            return new ProductForReadDTO
            {
                Id_Product = p.Id_Product,
                Name = p.Name,
                Id_Category = p.Id_Category
            };

        }
    }
}
