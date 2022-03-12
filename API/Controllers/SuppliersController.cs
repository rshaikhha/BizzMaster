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
    [Route("[controller]")]
    public class SuppliersController : Controller
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
        public async Task<ActionResult<SupplierDto>> GetSuppliers(int Id)
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

        private static SupplierDto ToDto(Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                title = supplier.Title,
                Country = supplier.Country.Title,
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

    }
}