using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class AuditController : BaseApiController
    {

        private readonly BMContext _context;
        private readonly ILogger<AuditController> _logger;

        public AuditController(BMContext context, ILogger<AuditController> logger)
        {
            this._context = context;
            _logger = logger;
        }



        [HttpGet("{id}/{year}/{month}")]
        public async Task<ActionResult<AuditDto>> Get(int Id,int year, int month)
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

            var forcast = await _context
            .SalesForecasts
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();

            var shipment = await _context
            .Orders
            .Include(x=>x.Items)
            .Where(x=>x.SupplyLineId == Id && x.Year == year && x.Month == month )
            .OrderByDescending(x=>x.CreatedOn)
            .FirstOrDefaultAsync();



            var items = new List<AuditItemDto>();
            foreach (var product in products)
            {
                var s = stock?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var f = forcast?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var sh = shipment?.Items.FirstOrDefault(x=>x.ProductId == product.Id)?.Quantity ?? 0;
                var shortage = Math.Max(f * 2 - s - sh, 0);

                items.Add(new AuditItemDto{
                    ProductId = product.Id,
                    OpenningStock = s,
                    Sales = f,
                    Order = sh,
                    Shortage = shortage,
                    ProductPartNumber = product.PartNumber
                });
            }

            return new AuditDto{
                SupplyLineId = Id,
                Year = year,
                Month = month,
                Items = items
            };
        }

        
    }
}