using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class CountryController : BaseApiController
    {

        private readonly BMContext _context;

        public CountryController(BMContext context)
        {
            this._context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        [HttpGet("abbreviations")]
        public async Task<ActionResult<List<string>>> GetAbbreviations()
        {
            return await _context.Countries.Select(x=>x.Abbr).ToListAsync();
        }
    }
}