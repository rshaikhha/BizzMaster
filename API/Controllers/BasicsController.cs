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
        public async Task<ActionResult<List<BrandDto>>> GetBrands(string typeHint = "")
        {
            return await _context.Brands
            .Where(x => x.Active && (string.IsNullOrEmpty(typeHint) || x.TypeHint.Contains(typeHint)))
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

        [HttpGet("usageTypes")]
        public async Task<ActionResult<List<UsageType>>> GetUsageTypes()
        {
            return await _context.UsageTypes.ToListAsync();
        }

        [HttpGet("MasterSystems")]
        public async Task<ActionResult<List<MasterSystem>>> GetMasterSystems()
        {
            return await _context.MasterSystems.ToListAsync();
        }

// update this method later
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            return await _context.Categories
            .Include(x => x.UsageType)
            .Include(x => x.MasterSystem)
            .Include(x => x.Children)
            
            .Select(x => ToCategoryDto(x))
            .ToListAsync();
        }


// update this method later
        private static CategoryDto ToCategoryDto(Category category)
        {


            return new CategoryDto()
            {
                Code = category.Code,
                Title = category.Title,
                Level = category.Level,
                ItemUnit = category.ItemUnit,
                SetUnit = category.SetUnit,
                UsageType = category.UsageType?.Title,
                MasterSystem = category.MasterSystem?.Title,
                HSCode = category.HSCode,
                Children = category.Children?.Select(x => ToCategoryDto(x)).ToList()
            };
        }
    }
}