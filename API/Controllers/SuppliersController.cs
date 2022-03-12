using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class SuppliersController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(BMContext context, ILogger<SuppliersController> logger)
        {
            this._context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierDto>>> GetSuppliers()
        {

            return await _context.Suppliers
                .Include(x => x.Country)
                .Include(x => x.Contacts)
                .Select(x => ToDto(x)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> GetSupplier(int Id)
        {
            var supplier = await _context.Suppliers
                // .Include(x => x.Country)
                // .Include(x => x.Contacts)
                .FindAsync(Id);

            if (supplier == null) return NotFound();


            await _context.Entry(supplier).Reference(x=>x.Country).LoadAsync();
            await _context.Entry(supplier).Collection(x=>x.Contacts).LoadAsync();

            return ToDto(supplier);

                
        }

        

        [HttpGet("lines")]
        public async Task<ActionResult<List<SupplyLineDto>>> GetSupplyLines()
        {

            return await _context.SupplyLines
                .Include(x => x.Supplier)
                .Include(x => x.Products).ThenInclude(x=>x.Brand)
                .Include(x => x.Products).ThenInclude(x=>x.Category)
                .Select(x => ToDto(x)).ToListAsync();
        }

        [HttpGet("lines/{id}")]
        public async Task<ActionResult<SupplyLineDto>> GetSupplyLines(int Id)
        {

            var supplyLine = await _context.SupplyLines
                .Include(x => x.Supplier)
                .Include(x => x.Products).ThenInclude(x=>x.Brand)
                .Include(x => x.Products).ThenInclude(x=>x.Category)
                .FirstOrDefaultAsync(x=>x.Id == Id);
                
                
            if (supplyLine == null) return NotFound();

            return ToDto(supplyLine);
        }


        [HttpGet("SalesForecast/{id}")]
        public async Task<ActionResult<List<SalesForecastDto>>> GetSalesForecasts(int Id)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new SalesForecastItem{ ProductId = x.Id, Quantity = 0}).ToList();


            var res = await _context
            .SalesForecasts
            .Include(x=>x.Items)
            .ThenInclude(x=>x.Product)
            .ThenInclude(x=>x.Brand)
            .Where(x=>x.SupplyLineId == Id)
            .ToListAsync();

            return res
            .GroupBy(x=>x.Title)
            .Select(g => g.OrderByDescending(f => f.CreatedOn).First())
            .Select(x=> ToDto(x))
            .ToList();


            
            
        }

        [HttpPost("SalesForecast")]
        public async Task<ActionResult<SalesForecast>> SetSalesForecast(SalesForecastUploadDto dto)
        {
            var supplyLine = _context.SupplyLines.Find(dto.SupplyLineId);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new SalesForecastItem{ ProductId = x.Id, Quantity = 0}).ToList();

            string title = string.Format("Forecast-{0}-{1}-{2}", dto.SupplyLineId , dto.Year, dto.Month);
            var prevforecast = _context.SalesForecasts
            .Where(x=> x.Title == title)
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefault();

            
            
            if (prevforecast != null)
            {
                foreach (var item in items)
                {
                    var previtem = prevforecast.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
                    if(previtem != null) item.Quantity = previtem.Quantity;
                }
            }

            foreach (var item in items)
            {
                var dtoItem = dto.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
                    if(dtoItem != null) item.Quantity = dtoItem.Quantity;
            }


            var forecast = new SalesForecast
            {
                Title = title,
                SupplyLineId = dto.SupplyLineId,
                Year = dto.Year,
                Month = dto.Month,
                Items = items.Where(x=>x.Quantity > 0).ToList(),

            };
            _context.SalesForecasts.Add(forecast);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return StatusCode(201);
            }
            return BadRequest(); 
            
        }




        private static SupplierDto ToDto(Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                title = supplier.Title,
                FullTitle = supplier.FullTitle,
                Country = supplier.Country.Title,
                Email = supplier.Email,
                Address = supplier.Address,
                Website = supplier.Website,
                Contacts = supplier.Contacts.Select(x => ToDto(x)).ToList()
            };


        }

        private static ContactDto ToDto(Contact contact)
        {

            return new ContactDto
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Initials = contact.Initials,
                Position = contact.Position,
                Email = contact.Email,
                Mobile = contact.Mobile,
                Mobile2 = contact.Mobile2,
                Mobile3 = contact.Mobile3,
                WhatsApp = contact.WhatsApp
            };
        }

        public static SupplyLineDto ToDto(SupplyLine supplyLine)
        {
            return new SupplyLineDto
            {
                Id = supplyLine.Id,
                Title = supplyLine.Title,
                Supplier = supplyLine.Supplier.Title,
                defaultPlanningType = supplyLine.defaultPlanningType.ToString(),
                Products = supplyLine.Products.OrderBy(x=>x.Order).Select(x=> ToDto(x)).ToList()
                
            };
        }

        private static ProductDto ToDto(Product product)
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

        private static SalesForecastDto ToDto(SalesForecast fc)
        {
            return new SalesForecastDto
            {
                SupplyLineId = fc.Id,
                Year = fc.Year,
                Month = fc.Month,
                Items = fc.Items.Select(x=> new SalesForecastItemDto{
                    ProductId = x.ProductId,
                    ProductTitle = x.Product.Description,
                    ProductPartNumner = x.Product.PartNumber,
                    ProductBrand = x.Product.Brand.Title,
                    Quantity = x.Quantity,
                }).ToList()
            };
        }

    }
}