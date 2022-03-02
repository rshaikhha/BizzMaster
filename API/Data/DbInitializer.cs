using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class DbInitializer
    {
        private static BMContext _context;

        public static async Task Initialize(BMContext context, UserManager<User> userManager)
        {
            _context = context;

            if(!userManager.Users.Any())
            {
                var user = new User
                {
                    UserName = "user",
                    Email = "user@bggt.ae"
                };
                await userManager.CreateAsync(user, "User@8827");
                await userManager.AddToRoleAsync(user, "Member");

                var admin = new User
                {
                    UserName = "admin",
                    Email = "admin@bggt.ae"
                };
                await userManager.CreateAsync(user, "Admin@8827");
                await userManager.AddToRolesAsync(user, new [] {"Member", "Admin"});
            }




            if (context.Countries.Any()) return;

            var Countries = CreateCountries();
            _context.Countries.AddRange(Countries);
            await _context.SaveChangesAsync();



            var brands = CreateBrands();
            _context.Brands.AddRange(brands);
            await _context.SaveChangesAsync();

            await CreateCategoriesAsync();
           
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

        private static List<Brand> CreateBrands()
        {
            var jp = _context.Countries.Single(x => x.Abbr == "JP").Id;
            var kr = _context.Countries.Single(x => x.Abbr == "KR").Id;
            var fr = _context.Countries.Single(x => x.Abbr == "FR").Id;
            var se = _context.Countries.Single(x => x.Abbr == "SE").Id;
            var de = _context.Countries.Single(x => x.Abbr == "DE").Id;

            var brands = new List<Brand>(){
                new Brand { Title="Toyota", CountryId = jp, TypeHint = "vehicle"},
                new Brand { Title="Hyundai", CountryId = kr, TypeHint = "vehicle"},
                new Brand { Title="Isuzu", CountryId = jp, TypeHint = "vehicle"},
                new Brand { Title="Peugeot", CountryId = jp, TypeHint = "vehicle"},
                new Brand { Title="Kia", CountryId = kr, TypeHint = "vehicle"},
                new Brand { Title="Renault", CountryId = fr, TypeHint = "vehicle"},
                new Brand { Title="Volvo", CountryId = se, TypeHint = "vehicle"},
                new Brand { Title="Nissan", CountryId = jp, TypeHint = "vehicle"},
                new Brand { Title="Mazda", CountryId = jp, TypeHint = "vehicle"},

                new Brand { Title="AISIN", CountryId = jp, TypeHint = "product,lubricant"},
                new Brand { Title="WOLVER", CountryId = de, TypeHint = "product,lubricant"},
                new Brand { Title="LIQUIMOLY", CountryId = de, TypeHint = "product,lubricant"},
                new Brand { Title="MOBIS", CountryId = kr, TypeHint = "product,lubricant"},

                new Brand { Title="HENDEL", CountryId = kr, TypeHint = "product,spare part"},
                new Brand { Title="DPH", CountryId = kr, TypeHint = "product,spare part"},       
            };

            brands.ForEach(x =>
            {
                x.LogoImage = "Images/Brands/" + x.Title + ".jpg";
            }
            );
            return brands;
        }

        private static async Task CreateCategoriesAsync()
        {

            var cat1 = new Category { Title="Lubricant", TypeHint = "product"};
            var cat2 = new Category { Title="Spare Part", TypeHint = "product"};

            _context.Categories.Add(cat1);
            _context.Categories.Add(cat2);

            await _context.SaveChangesAsync();



            var cat11 = new Category { Title="Engine Oil", Parent = cat1, TypeHint = "product"};
            var cat12 = new Category { Title="Transmission Fluid", Parent = cat1, TypeHint = "product"};
            var cat13 = new Category { Title="Gear Oil", Parent = cat1, TypeHint = "product"};
            var cat14 = new Category { Title="Brake Fluid", Parent = cat1, TypeHint = "product"};
            var cat15 = new Category { Title="Coolant", Parent = cat1, TypeHint = "product"};

            var cat21 = new Category { Title="Engine Parts", Parent = cat2, TypeHint = "product"};
            var cat22 = new Category { Title="Suspension Parts", Parent = cat2, TypeHint = "product"};
            var cat23 = new Category { Title="Body Parts", Parent = cat2, TypeHint = "product"};
            var cat24 = new Category { Title="Electrical Parts", Parent = cat2, TypeHint = "product"};


            _context.Categories.AddRange(
                cat11,cat12,cat13,cat14,cat15,
                cat21,cat22,cat23,cat24
            );

            await _context.SaveChangesAsync();
        }

    }
}