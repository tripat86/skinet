using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productsTypeRepo;
        private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepo,
                                IGenericRepository<ProductType> productsTypeRepo,
                                IGenericRepository<ProductBrand> productsBrandRepo,
                                IMapper mapper)
        {
            _mapper = mapper;
            _productsBrandRepo = productsBrandRepo;
            _productsTypeRepo = productsTypeRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            var spec = new ProductWithBrandsAndTypesSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecification(id);
            
            var product = await _productsRepo.GetEntityWithSpec(spec);
            
            if(product == null) return NotFound(new ApiResponse(404));

            var productToReturn = _mapper.Map<Product, ProductToReturnDto>(product);

            return Ok(productToReturn);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrandsToReturn = await _productsBrandRepo.ListAllAsync();

            return Ok(productBrandsToReturn);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypesToReturn = await _productsTypeRepo.ListAllAsync();

            return Ok(productTypesToReturn);
        }
    }
}