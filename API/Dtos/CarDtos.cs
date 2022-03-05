using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PlatformDto
    {
        public string Title { get; set; } 
        public string BrandTitle { get; set; }
        public string CountryName { get; set; }
        public string CountryAbbr { get; set; }
        public string BrandLogoImage { get; set; }
        public string CountryFlagImage { get; set; }
        public bool Active { get; set; }
        
    }

    public class CarDto
    {
        public string Title { get; set; } 
        public string platformTitle { get; set; } 
        public string BrandTitle { get; set; }
        public string CountryName { get; set; }
        public string CountryAbbr { get; set; }
        public string BrandLogoImage { get; set; }
        public string CountryFlagImage { get; set; }
        public bool Active { get; set; }
    }
}