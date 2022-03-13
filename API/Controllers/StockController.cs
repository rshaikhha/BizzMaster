using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class StockController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<StockController> _logger;

        public StockController(BMContext context, ILogger<StockController> logger)
        {
            this._context = context;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<StockDto>>> Get(int Id)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new StockItem{ ProductId = x.Id, Quantity = 0}).ToList();


            var res = await _context
            .Stocks
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
        public async Task<ActionResult<List<StockDto>>> GetHistory(int Id,int year, int month)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new StockItem{ ProductId = x.Id, Quantity = 0}).ToList();


            var res = await _context
            .Stocks
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
        public async Task<ActionResult<StockDto>> Get(int Id,int year, int month)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new StockItem{ ProductId = x.Id, Quantity = 0}).ToList();


            var res = await _context
            .Stocks
            .Include(x=>x.Items)
            .ThenInclude(x=>x.Product)
            .ThenInclude(x=>x.Brand)
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .ToListAsync();

            var st = res
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefault();

            if (st == null)
            {
                return null;
            }
            return ToDto(st);
        }


        [HttpPost]
        public async Task<ActionResult<Stock>> Post(StockUploadDto dto)
        {

            if(!dto.Items.Any()) return BadRequest(new ProblemDetails{Title = "No Items!!!"});

            var supplyLine = _context.SupplyLines.Find(dto.SupplyLineId);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            // _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            // var products = supplyLine.Products;
            // var items = products.Select(x=> new StockItem{ ProductId = x.Id, Quantity = 0}).ToList();

            string title = string.Format("Stock-{0}-{1}-{2}", dto.SupplyLineId , dto.Year, dto.Month);
            // var prevstock = _context.Stocks
            // .Include(x=>x.Items)
            // .Where(x=> x.Title == title)
            // .OrderByDescending(x=>x.CreatedOn)
            // .FirstOrDefault();

            
            
            // if (prevstock != null)
            // {
            //     foreach (var item in items)
            //     {
            //         var previtem = prevstock.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
            //         if(previtem != null) item.Quantity = previtem.Quantity;
            //     }
            // }

            // foreach (var item in items)
            // {
            //     var dtoItem = dto.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
            //         if(dtoItem != null) item.Quantity = dtoItem.Quantity;
            // }


            var stock = new Stock
            {
                Title = title,
                SupplyLineId = dto.SupplyLineId,
                Year = dto.Year,
                Month = dto.Month,
                Items = dto.Items.Where(x=>x.Quantity > 0).Select(x=>new StockItem{
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                } ).ToList(),

            };
            _context.Stocks.Add(stock);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return StatusCode(201);
            }
            return BadRequest(); 
            
        }


        [HttpGet("Calculate/{id}/{year}/{month}")]
        public async Task<ActionResult<StockDto>> Calculate(int Id,int year, int month)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            var items = products.Select(x=> new StockItem{ ProductId = x.Id, Quantity = 0}).ToList();

            var lastyear = month > 1 ? year : year - 1;
            var lastmonth = month > 1 ? month - 1 : 12;
            
            var lastStock = await _context
            .Stocks
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == lastyear && x.Month == lastmonth )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            var lastForcast = await _context
            .SalesForecasts
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == lastyear && x.Month == lastmonth )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            var lastShipment = await _context
            .Orders
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == lastyear && x.Month == lastmonth )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            string title = string.Format("Stock-{0}-{1}-{2}", Id , year, month);
            var stockitems = new List<StockItem>();
            foreach (var product in products)
            {
                var s = lastStock?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var f = lastForcast?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var sh = lastShipment?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;

                var q = Math.Max(0, s + sh - f);

                if (q > 0)
                stockitems.Add(new StockItem{ProductId = product.Id, Quantity = q});
                
            }
            var stock = new StockDto
            {
                SupplyLineId = Id,
                Year = year,
                Month = month,
                Items = stockitems.Where(x=>x.Quantity > 0).Select(x=>new StockItemDto{
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };

            
            return stock;


        }




        private static StockDto ToDto(Stock stock)
        {
            return new StockDto
            {
                SupplyLineId = stock.SupplyLine.Id,
                Year = stock.Year,
                Month = stock.Month,
                Items = stock.Items.Select(x=> new StockItemDto{
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