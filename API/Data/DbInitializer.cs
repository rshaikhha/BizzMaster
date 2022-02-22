using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class DbInitializer
    {
        private static BMContext _context;

        public static async Task Initialize(BMContext context)
        {
            _context = context;
            if (context.Countries.Any()) return;

            var Countries = CreateCountries();
            _context.Countries.AddRange(Countries);

            var brands = CreateVehicleBrands();
            _context.VehicleBrands.AddRange(brands);

            
            await _context.SaveChangesAsync();
        }


        private static List<Country> CreateCountries()
        {
            var Countries = new List<Country>(){

                new Country { Title= "China", Abbr = "CN", FlagImageUrl = "Images/Flags/CN.jpg" },
                new Country { Title= "Germany", Abbr = "DE", FlagImageUrl = "Images/Flags/DE.jpg" },
                new Country { Title= "Iran", Abbr = "IR", FlagImageUrl = "Images/Flags/IR.jpg" },
                new Country { Title= "Japan", Abbr = "JP", FlagImageUrl = "Images/Flags/JP.jpg" },
                new Country { Title= "Republic of Korea", Abbr = "KR", FlagImageUrl = "Images/Flags/KR.jpg" },
                new Country { Title= "Turkey", Abbr = "TR", FlagImageUrl = "Images/Flags/TR.jpg" },
                new Country { Title= "Ukraine", Abbr = "UA", FlagImageUrl = "Images/Flags/UA.jpg" },
                new Country { Title= "United Arab Emirates", Abbr = "AE", FlagImageUrl = "Images/Flags/AE.jpg" },
                new Country { Title= "France", Abbr = "FR", FlagImageUrl = "Images/Flags/FR.jpg" },
                new Country { Title= "Sweden", Abbr = "SE", FlagImageUrl = "Images/Flags/SE.jpg" },
            };

            return Countries;
        }

        private static List<VehicleBrand> CreateVehicleBrands()
        {
            var jp = _context.Countries.Single(x => x.Abbr == "JP").Id;
            var kr = _context.Countries.Single(x => x.Abbr == "KR").Id;
            var fr = _context.Countries.Single(x => x.Abbr == "FR").Id;
            var se = _context.Countries.Single(x => x.Abbr == "SE").Id;

            var brands = new List<VehicleBrand>(){
                new VehicleBrand { Title="Toyota", CountryId = jp},
                new VehicleBrand { Title="Hyundai", CountryId = kr},
                new VehicleBrand { Title="Isuzu", CountryId = jp},
                new VehicleBrand { Title="Peugeot", CountryId = jp},
                new VehicleBrand { Title="Kia", CountryId = kr},
                new VehicleBrand { Title="Renault", CountryId = fr},
                new VehicleBrand { Title="Volvo", CountryId = se},
                new VehicleBrand { Title="Nissan", CountryId = jp},
                new VehicleBrand { Title="Mazda", CountryId = jp},
            };

            return brands;
        }

    }
}