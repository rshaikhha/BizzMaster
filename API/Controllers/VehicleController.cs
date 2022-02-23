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









    }
}