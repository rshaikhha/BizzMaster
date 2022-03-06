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
        public async Task<ActionResult<List<BrandDto>>> GetBrands()
        {
            return await _context.Brands.Where(x=>x.TypeHint.Contains("#car"))
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

        [HttpGet("Platforms")]
        public async Task<ActionResult<List<PlatformDto>>> GetPlatforms()
        {
           

            return await _context.Platforms
                .Where(x=>x.Active)
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

        [HttpGet("BrandPlatforms/{BrandTitle}")]
        public async Task<ActionResult<List<PlatformDto>>> GetBrandPlatforms(string BrandTitle)
        {
            var brandId = (await _context.Brands.FirstOrDefaultAsync(x=>x.Title == BrandTitle))?.Id;

            return await _context.Platforms
                .Where(x=>x.Active)
                .Where(x=> x.BrandId == brandId)
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

        [HttpGet("Cars")]
        public async Task<ActionResult<List<CarDto>>> GetCars()
        {


            return await _context.Cars
            .Where(x=>x.Active)
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

        [HttpGet("PlatformCars/{PlatformTitle}")]
        public async Task<ActionResult<List<CarDto>>> GetPlatformCars(string PlatformTitle)
        {
            
            var PlatformId = (await _context.Platforms.FirstOrDefaultAsync(x=>x.Title == PlatformTitle))?.Id;


            return await _context.Cars
            .Where(x=>x.Active)
            .Where(x=> x.PlatformId == PlatformId)
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