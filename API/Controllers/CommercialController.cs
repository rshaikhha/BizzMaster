using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class CommercialController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<CommercialController> _logger;

        public CommercialController(BMContext context, ILogger<CommercialController> logger)
        {
            this._context = context;
            _logger = logger;
        }


        [HttpGet("cards")]
        public async Task<ActionResult<List<CommercialCardDto>>> Cards()
        {
            return await _context.CommercialCards
            .Select(x=> new CommercialCardDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IssueDate = x.IssueDate,
                ValidityDate = x.ValidityDate,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToListAsync();
        }

        [HttpGet("cards/{id}")]
        public async Task<ActionResult<CommercialCardDto>> Cards(int id)
        {
            var card = await _context.CommercialCards.FindAsync(id);
            if (card == null) return BadRequest();

            return 
            new CommercialCardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                IssueDate = card.IssueDate,
                ValidityDate = card.ValidityDate,
                FirstName = card.FirstName,
                LastName = card.LastName
            };

        }

        [HttpGet]
        public async Task<ActionResult<List<OrderRegistrationDto>>> Get()
        {
            return await _context.OrderRegistrations
            .Include(x=>x.CommercialCard)
            .Include(x=> x.Categories)
            .Select(x=> new OrderRegistrationDto
            {
                Id = x.Id,
                Title = x.Title,
                IssueDate = x.IssueDate,
                ValidityDate = x.ValidityDate,
                CommercialCardId = x.CommercialCardId,
                CommercialCardTitle = x.CommercialCard.Title,
                DocumentNumber = x.DocumentNumber,
                RegistrationNumber = x.RegistrationNumber,
                Currency = x.Currency,
                Amount = x.Amount,
                Unit = x.Unit,
                Quantity = x.Quantity,
                Categories = string.Join(',', x.Categories.Select(c => c.Title).ToList()),
                OrderRegistrationStatus = x.OrderRegistrationStatus.ToString()

            })
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderRegistrationDto>> Get(int id)
        {
            var reg = await _context.OrderRegistrations.FindAsync(id);
            if (reg == null)  return BadRequest();
            _context.Entry(reg).Reference(x=>x.CommercialCard).Load();
            _context.Entry(reg).Collection(x=>x.Categories).Load();

            return new OrderRegistrationDto
            {
                Id = reg.Id,
                Title = reg.Title,
                IssueDate = reg.IssueDate,
                ValidityDate = reg.ValidityDate,
                CommercialCardId = reg.CommercialCardId,
                CommercialCardTitle = reg.CommercialCard.Title,
                DocumentNumber = reg.DocumentNumber,
                RegistrationNumber = reg.RegistrationNumber,
                Currency = reg.Currency,
                Amount = reg.Amount,
                Unit = reg.Unit,
                Quantity = reg.Quantity,
                Categories = string.Join(',', reg.Categories.Select(c => c.Title).ToList()),
                OrderRegistrationStatus = reg.OrderRegistrationStatus.ToString()

            };
        }




        
    }
}