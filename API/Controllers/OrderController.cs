using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<OrderController> _logger;

        public OrderController(BMContext context, ILogger<OrderController> logger)
        {
            this._context = context;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<List<OrderDto>>> Get(int Id)
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
            .Orders
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
        public async Task<ActionResult<List<OrderDto>>> GetHistory(int Id,int year, int month)
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
            .Orders
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
        public async Task<ActionResult<OrderDto>> Get(int Id,int year, int month)
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
            .Orders
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
        public async Task<ActionResult<Order>> Post(OrderUploadDto dto)
        {

            if(!dto.Items.Any()) return BadRequest(new ProblemDetails{Title = "No Items!!!"});

            var supplyLine = _context.SupplyLines.Find(dto.SupplyLineId);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            // _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            // var products = supplyLine.Products;
            // var items = products.Select(x=> new OrderItem{ ProductId = x.Id, Quantity = 0}).ToList();

            string title = string.Format("Order-{0}-{1}-{2}", dto.SupplyLineId , dto.Year, dto.Month);
            // var prevOrder = _context.Orders
            // .Include(x=>x.Items)
            // .Where(x=> x.Title == title)
            // .OrderByDescending(x=>x.CreatedOn)
            // .FirstOrDefault();

            
            
            // if (prevOrder != null)
            // {
            //     foreach (var item in items)
            //     {
            //         var previtem = prevOrder.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
            //         if(previtem != null) item.Quantity = previtem.Quantity;
            //     }
            // }

            // foreach (var item in items)
            // {
            //     var dtoItem = dto.Items.FirstOrDefault(x=>x.ProductId == item.ProductId);
            //         if(dtoItem != null) item.Quantity = dtoItem.Quantity;
            // }


            var order = new Order
            {
                Title = title,
                SupplyLineId = dto.SupplyLineId,
                Year = dto.Year,
                Month = dto.Month,
                Items = dto.Items.Where(x=>x.Quantity > 0).Select(x=> new OrderItem{
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList(),

            };
            _context.Orders.Add(order);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {
                return StatusCode(201);
            }
            return BadRequest(); 
            
        }


        [HttpGet("calculate/{id}/{year}/{month}")]
        public async Task<ActionResult<OrderDto>> Calculate(int Id,int year, int month)
        {

            var supplyLine = _context.SupplyLines.Find(Id);
            if (supplyLine == null)
            {
                return BadRequest();
            }
            _context.Entry(supplyLine).Collection(x=>x.Products).Load();
            var products = supplyLine.Products;
            


            var stock = await _context
            .Stocks
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            var sales = await _context
            .SalesForecasts
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            string title = string.Format("Order-{0}-{1}-{2}", Id , year, month);
            var orderItems = new List<OrderItemDto>();

            foreach (var product in products)
            {
                var s = stock?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var f = sales?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;

                var q = Math.Max(0, 2 * f - s);

                if (q > 0)
                orderItems.Add(new OrderItemDto{ProductId = product.Id, Quantity = q});
                
            }

            return new OrderDto{
                SupplyLineId = Id,
                Month = month,
                Year = year,
                Items = orderItems
            };
        }



        private static OrderDto ToDto(Order order)
        {
            return new OrderDto
            {
                SupplyLineId = order.SupplyLine.Id,
                Year = order.Year,
                Month = order.Month,
                Items = order.Items.Select(x=> new OrderItemDto{
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