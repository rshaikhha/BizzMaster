using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class CarController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<CarController> _logger;

        public CarController(BMContext context, ILogger<CarController> logger)
        {
            this._context = context;
            _logger = logger;
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            return await _context.Brands.Where(x=>x.TypeHint.Contains("#car")).ToListAsync();
        }

        [HttpGet("Platforms/{BrandTitle}")]
        public async Task<ActionResult<List<PlatformDto>>> GetPlatforms(string BrandTitle)
        {
            var brandId = (await _context.Brands.FirstOrDefaultAsync(x=>x.Title == BrandTitle))?.Id;

            return await _context.Platforms
                .Where(x=>x.Active)
                .Where(x=> String.IsNullOrEmpty(BrandTitle) || x.BrandId == brandId)
                .Include(x=>x.Brand).ThenInclude(x=>x.Country)
                .Select(x=>new PlatformDto{
                    Title = x.Title,
                    BrandTitle = x.Brand.Title,
                    CountryName = x.Brand.Country.Title,
                    CountryAbbr = x.Brand.Country.Abbr,
                    BrandLogoImage = x.Brand.LogoImage,
                    CountryFlagImage = x.Brand.Country.FlagImageUrl
                })
                .ToListAsync();
        }

        [HttpGet("Cars/{PlatformTitle}")]
        public async Task<ActionResult<List<CarDto>>> GetCars(string PlatformTitle)
        {
            
            var PlatformId = (await _context.Platforms.FirstOrDefaultAsync(x=>x.Title == PlatformTitle))?.Id;


            return await _context.Cars
            .Where(x=>x.Active)
            .Where(x=> string.IsNullOrEmpty(PlatformTitle) || x.PlatformId == PlatformId)
            .Include(x=>x.Platform)
            .ThenInclude(x=>x.Brand)
            .Select(x=>new CarDto{
                    Title = x.Title,
                    platformTitle = x.Platform.Title,
                    BrandTitle = x.Platform.Brand.Title,
                    CountryName = x.Platform.Brand.Country.Title,
                    CountryAbbr = x.Platform.Brand.Country.Abbr,
                    BrandLogoImage = x.Platform.Brand.LogoImage,
                    CountryFlagImage = x.Platform.Brand.Country.FlagImageUrl
                })
            .ToListAsync();
        }

        

        

        









    }
}