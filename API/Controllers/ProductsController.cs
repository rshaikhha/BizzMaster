using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.RequestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {

        private readonly BMContext _context;

        public ProductsController(BMContext context)
        {
            this._context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery]ProductParams productParams)
        {
             var query = _context.Products
             .Include(x=> x.Brand)
            .Include(x=> x.Category).AsQueryable();

            query = query
                .Sort(productParams.OrderBy)
                .Search(productParams.SearchTerm)
                .Filter(productParams.Brands, productParams.Categories)
                .AsQueryable();

            var products = await PagedList<Product>.ToPagedList(
                query, 
                productParams.PageNumber, 
                productParams.PageSize);
            Response.AddPaginationHeader(products.MetaData);
            return products.Select(x=>ToDto(x)).ToList();


            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);


            if (product == null) return NotFound();
            _context.Entry(product).Reference(x=>x.Brand).Load();
            _context.Entry(product).Reference(x=>x.Category).Load();
            return ToDto(product);
            
        }

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
            var brands = await _context.Brands.Where(x=>x.TypeHint.Contains("#product"))
                .Select(p=>new {title = p.Title , id = p.Id})
                .ToListAsync();
            var categories = await _context.Categories.Where(x=>x.Level == 1)
                .Select(p=>new {title = p.Title, id = p.Id})
                .Distinct()
                .ToListAsync();

                return Ok(new {brands, categories});
        }


        private ProductDto ToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                PartNumber = product.PartNumber,
                Description = product.Description,
                Brand = product.Brand.Title,
                Category = product.Category.Title,

                ItemVolume = product.ItemVolume,
                ItemWeight = product.ItemWeight,
                ItemPerSet = product.ItemPerSet,
                Order = product.Order
            };
        }

    }
}