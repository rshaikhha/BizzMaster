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

        [HttpGet("Platforms")]
        public async Task<ActionResult<List<Platform>>> GetPlatforms(int? BrandId)
        {
            return await _context.Platforms.Where(x=> BrandId == null || x.BrandId == BrandId).Include(x=>x.Brand).ToListAsync();
        }

        [HttpGet("Cars")]
        public async Task<ActionResult<List<Car>>> GetCars(int? PlatformId)
        {
            return await _context.Cars.Where(x=> PlatformId == null || x.PlatformId == PlatformId).Include(x=>x.Platform).ThenInclude(x=>x.Brand).ToListAsync();
        }

        

        

        









    }
}