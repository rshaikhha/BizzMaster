using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class BasicsController : BaseApiController
    {
        private readonly BMContext _context;

        public BasicsController(BMContext context)
        {
            this._context = context;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<BrandDto>>> GetBrands()
        {
            return await _context.Brands
            .Where(x => x.Active)
            .Include(x => x.Country)
            .Select(x => new BrandDto
            {
                Title = x.Title,
                CountryName = x.Country.Title,
                CountryAbbr = x.Country.Abbr,
                LogoImage = x.LogoImage,
                CountryFlagImage = x.Country.FlagImageUrl,
                Active = x.Active

            })
            .ToListAsync();
        }


        [HttpGet("countries")]
        public async Task<ActionResult<List<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }
        
    }
}