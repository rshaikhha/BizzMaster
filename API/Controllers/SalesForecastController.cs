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
    public class SalesForecastController : BaseApiController
    {

        private readonly BMContext _context;
        private readonly ILogger<SalesForecastController> _logger;

        public SalesForecastController(BMContext context, ILogger<SalesForecastController> logger)
        {
            this._context = context;
            _logger = logger;
        }



        [HttpGet("{id}")]
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
            .OrderBy(x=>x.Title)
            .Select(x=> ToDto(x))
            .ToList();


            
            
        }

        [HttpGet("history/{id}/{year}/{month}")]
        public async Task<ActionResult<List<SalesForecastDto>>> GetSalesForecastHistory(int Id,int year, int month)
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
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .ToListAsync();

            return res
            
            .Select(x=> ToDto(x))
            .ToList();


            
            
        }

        [HttpGet("{id}/{year}/{month}")]
        public async Task<ActionResult<SalesForecastDto>> GetSalesForecast(int Id,int year, int month)
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
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .ToListAsync();

            var fc = res
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefault();

            if (fc == null)
            {
                return null;
            }
            return ToDto(fc);
        }

        

        [HttpPost]
        public async Task<ActionResult<SalesForecast>> SetSalesForecast(SalesForecastUploadDto dto)
        {

            if(!dto.Items.Any()) return BadRequest(new ProblemDetails{Title = "No Items!!!"});

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
            .Include(x=>x.Items)
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

        private static SalesForecastDto ToDto(SalesForecast fc)
        {
            return new SalesForecastDto
            {
                SupplyLineId = fc.SupplyLine.Id,
                Year = fc.Year,
                Month = fc.Month,
                Items = fc.Items.Select(x=> new SalesForecastItemUploadDto{
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