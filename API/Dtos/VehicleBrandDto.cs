using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class VehicleBrandDto
    {
        public string Title { get; set; }

        public string CountryName {get; set;}
        public string CountryAbbr { get; set; }
        public string LogoImage { get; set; }
        public string CountryFlagImage { get; set; }
        public bool Active { get; set; }
        
    }
}