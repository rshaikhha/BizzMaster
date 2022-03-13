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


        

        [HttpGet("Activeproducts/{id}")]
        public async Task<ActionResult<List<ProductDto>>> GetActiveProducts(int Id)
        {


            var res = await _context
            .SalesForecasts
            .Include(x=>x.Items)
            .ThenInclude(x=>x.Product)
            .Where(x=>x.SupplyLineId == Id)
            .ToListAsync();

            var pp = res
            .GroupBy(x=>x.Title)
            .Select(g => g.OrderByDescending(f => f.CreatedOn).First())
            .SelectMany(x=> x.Items)
            .GroupBy(x=>x.ProductId)
            .Select(g=> new { Product = g.First().Product , quantity = g.Sum(p=>p.Quantity)})
            .Where(x=>x.quantity > 0)
            .Select(x=>ToDto(x.Product))
            .OrderBy(x=>x.Order)
            .ToList();

            return pp;
            
            
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
                PartNumber = product?.PartNumber,
                Description = product?.Description,
                Brand = product?.Brand?.Title,
                Category = product?.Category?.Title,

                ItemVolume = (product!= null) ? product.ItemVolume : 0,
                ItemWeight = (product!= null) ? product.ItemWeight : 0,
                ItemPerSet = (product!= null) ? product.ItemPerSet : 0,
                Order = (product!= null) ? product.Order : 10000,
            };
        }

        

    }
}