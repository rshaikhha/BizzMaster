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
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class VehicleController : BaseApiController
    {
        private readonly BMContext _context;
        private readonly ILogger<VehicleController> _logger;

        public VehicleController(BMContext context, ILogger<VehicleController> logger)
        {
            this._context = context;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<VehicleBrand>>> GetBrands()
        {
            return await _context.VehicleBrands.ToListAsync();
        }


        [HttpPost("ImportBrands")]
        public async Task<ActionResult<List<VehicleBrand>>> ImportBrands([FromBody] List<VehicleBrandDto> values)
        {

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(new ProblemDetails { Title = message });
            }
            var brands = await CreateBrandListFromDto(values);
            await _context.VehicleBrands.AddRangeAsync(brands);
            var result = await _context.SaveChangesAsync() > 0;

            if (result)
            {
                return StatusCode(201);
            }
            return BadRequest(new ProblemDetails { Title = "Cannot Read The Imported Data" });
        }



        private async Task<List<VehicleBrand>> CreateBrandListFromDto(List<VehicleBrandDto> Dtos)
        {
            var countries = await _context.Countries.ToListAsync();
            var list = new List<VehicleBrand>();
            Dtos = Dtos.Where(x => !_context.VehicleBrands.Any(b => b.Title == x.Title)).ToList();
            foreach (var item in Dtos)
            {
                try
                {
                    var brand = new VehicleBrand()
                    {
                        Title = item.Title,
                        CountryId = countries.Single(x => x.Abbr == item.CountryAbbr).Id,
                        LogoImage = string.IsNullOrEmpty(item.LogoImage) ? item.Title + ".jpg" : item.LogoImage,
                        Active = item.Active
                    };
                    list.Add(brand);
                }
                catch (Exception) { }
            }

            return list;
        }
    }
}