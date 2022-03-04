using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Data
{
    public class DbInitializer
    {
        private static BMContext _context;

        public static async Task Initialize(BMContext context, UserManager<User> userManager)
        {
            _context = context;

            if (!userManager.Users.Any())
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
                await userManager.CreateAsync(admin, "Admin@8827");
                await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
            }




            if (context.Countries.Any()) return;

            var cats = CreateCategories();
            context.Categories.AddRange(cats);
            context.SaveChanges();

            // var bcats = CreateBaseCategories();
            // context.Categories.AddRange(bcats);
            // context.SaveChanges();

            // var scats = CreateSubCategories();
            // context.Categories.AddRange(scats);
            // context.SaveChanges();

            var Countries = CreateCountries();
            context.Countries.AddRange(Countries);
            context.SaveChanges();

            var brands = CreateBrands();
            context.Brands.AddRange(brands);
            context.SaveChanges();

            var plats = createPlatforms();
            context.Platforms.AddRange(plats);
            context.SaveChanges();

            var cars = CreateCars();
            context.Cars.AddRange(cars);
            context.SaveChanges();

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
                new Brand { Title="Toyota", CountryId = jp, TypeHint = "#car"},
                new Brand { Title="Hyundai", CountryId = kr, TypeHint = "#car"},
                new Brand { Title="Isuzu", CountryId = jp, TypeHint = "#car"},
                new Brand { Title="Peugeot", CountryId = jp, TypeHint = "#car"},
                new Brand { Title="Kia", CountryId = kr, TypeHint = "#car"},
                new Brand { Title="Renault", CountryId = fr, TypeHint = "#car"},
                new Brand { Title="Volvo", CountryId = se, TypeHint = "#car"},
                new Brand { Title="Nissan", CountryId = jp, TypeHint = "#car"},
                new Brand { Title="Mazda", CountryId = jp, TypeHint = "#car"},

                new Brand { Title="AISIN", CountryId = jp, TypeHint = "#product,#lubricant"},
                new Brand { Title="WOLVER", CountryId = de, TypeHint = "#product,#lubricant"},
                new Brand { Title="LIQUIMOLY", CountryId = de, TypeHint = "#product,#lubricant"},
                new Brand { Title="MOBIS", CountryId = kr, TypeHint = "#product,#lubricant"},

                new Brand { Title="HENDEL", CountryId = kr, TypeHint = "#product,#sparepart"},
                new Brand { Title="DPH", CountryId = kr, TypeHint = "#product,#sparepart"},
            };

            brands.ForEach(x =>
            {
                x.LogoImage = "Images/Brands/" + x.Title + ".jpg";
            }
            );
            return brands;
        }

        private static List<Platform> createPlatforms()
        {
            var ty = _context.Brands.Single(x => x.Title == "Toyota").Id;
            var hy = _context.Brands.Single(x => x.Title == "Hyundai").Id;
            var Is = _context.Brands.Single(x => x.Title == "Isuzu").Id;
            var Pe = _context.Brands.Single(x => x.Title == "Peugeot").Id;
            var Ki = _context.Brands.Single(x => x.Title == "Kia").Id;
            var Re = _context.Brands.Single(x => x.Title == "Renault").Id;
            var Vo = _context.Brands.Single(x => x.Title == "Volvo").Id;
            var Ni = _context.Brands.Single(x => x.Title == "Nissan").Id;
            var Ma = _context.Brands.Single(x => x.Title == "Mazda").Id;

            var plats = new List<Platform>
            {
                new Platform{ Title = "Corolla", BrandId = ty},
                new Platform{ Title = "Land Cruiser", BrandId = ty},
                new Platform{ Title = "Land Cruiser Prado", BrandId = ty},
                new Platform{ Title = "Camry", BrandId = ty},
                new Platform{ Title = "Yaris", BrandId = ty},

                new Platform{ Title = "Accent", BrandId = hy},
                new Platform{ Title = "Elentra", BrandId = hy},
                new Platform{ Title = "Sonata", BrandId = hy},
                new Platform{ Title = "tucson", BrandId = hy},
                new Platform{ Title = "ix55", BrandId = hy},

                new Platform{ Title = "Trooper", BrandId = Is},
                new Platform{ Title = "D-Max", BrandId = Is},
                new Platform{ Title = "N-series", BrandId = Is},
                new Platform{ Title = "F-series, FSR/FRR", BrandId = Is},
                new Platform{ Title = "F-series, FSR/FRR", BrandId = Is},

                new Platform{ Title = "206", BrandId = Pe},
                new Platform{ Title = "207", BrandId = Pe},
                new Platform{ Title = "405", BrandId = Pe},
                new Platform{ Title = "2008", BrandId = Pe},

                new Platform{ Title = "Rio", BrandId = Ki},
                new Platform{ Title = "Cerato", BrandId = Ki},
                new Platform{ Title = "Picanto", BrandId = Ki},
                new Platform{ Title = "Sportage", BrandId = Ki},
                new Platform{ Title = "Optima", BrandId = Ki},
                new Platform{ Title = "Mohave", BrandId = Ki},

                new Platform{ Title = "Captur", BrandId = Re},
                new Platform{ Title = "Symbol", BrandId = Re},
                new Platform{ Title = "Sandero", BrandId = Re},
                new Platform{ Title = "Logan", BrandId = Re},
                new Platform{ Title = "Megan", BrandId = Re},
                new Platform{ Title = "Fluence", BrandId = Re},

                new Platform{ Title = "XC-60", BrandId = Vo},
                new Platform{ Title = "XC-70", BrandId = Vo},
                new Platform{ Title = "XC-90", BrandId = Vo},
                new Platform{ Title = "FH", BrandId = Vo},
                new Platform{ Title = "FH-16", BrandId = Vo},
                new Platform{ Title = "FMX", BrandId = Vo},

                new Platform{ Title = "Juke", BrandId = Ni},
                new Platform{ Title = "X-Trail", BrandId = Ni},
                new Platform{ Title = "Qashqai", BrandId = Ni},

                new Platform{ Title = "2", BrandId = Ma},
                new Platform{ Title = "3", BrandId = Ma},
                new Platform{ Title = "323", BrandId = Ma},
                new Platform{ Title = "MX-6", BrandId = Ma},
            };
                
            return plats;
        }

        private static List<Car> CreateCars()
        {
            var Cor = _context.Platforms.Single(x => x.Title == "Corolla").Id;
            var Cam = _context.Platforms.Single(x => x.Title == "Camry").Id;

            var cars = new List<Car>
            {
                new Car{ Title = "Corolla 1.2T", PlatformId = Cor},
                new Car{ Title = "Corolla 1.6 VVT-i", PlatformId = Cor},
                new Car{ Title = "Corolla 1.8 Hybrid", PlatformId = Cor},
                new Car{ Title = "Corolla 2.0 Hybrid", PlatformId = Cor},

                new Car{ Title = "Camry 1.8", PlatformId = Cam},
                new Car{ Title = "Camry 2.0i", PlatformId = Cam},
                new Car{ Title = "Camry 2.0i 4WD", PlatformId = Cam},
                new Car{ Title = "Camry 2.2i", PlatformId = Cam},
                new Car{ Title = "Camry 2.4 16V VVT-i", PlatformId = Cam},
                new Car{ Title = "Camry 2.5i", PlatformId = Cam}
            };

            return cars;


        }

        private static List<Category> CreateCategories()
        {

            var cats = new List<Category>{

            new Category { Title="Lubricant", TypeHint = "product" , Children = new List<Category>{
                new Category { Title="Engine Oil", TypeHint = "product"},
                new Category { Title="Transmission Fluid",TypeHint = "product"},
                new Category { Title="Gear Oil", TypeHint = "product"},
                new Category { Title="Brake Fluid", TypeHint = "product"},
                new Category { Title="Coolant", TypeHint = "product"},
            }},

            new Category { Title="Spare Part", TypeHint = "product", Children = new List<Category>{
                new Category { Title="Engine Parts", TypeHint = "product"},
                new Category { Title="Suspension Parts", TypeHint = "product"},
                new Category { Title="Body Parts", TypeHint = "product"},
                new Category { Title="Electrical Parts", TypeHint = "product"},
            }}
            };

            return cats;
        }
        private static List<Category> CreateBaseCategories()
        {

            var cats = new List<Category>{

            new Category { Title="Lubricant", TypeHint = "product"},
            new Category { Title="Spare Part", TypeHint = "product"},

            // new Category { Title="Engine Oil", ParentCategoryId = 1, TypeHint = "product"},
            // new Category { Title="Transmission Fluid", ParentCategoryId = 1, TypeHint = "product"},
            // new Category { Title="Gear Oil", ParentCategoryId = 1, TypeHint = "product"},
            // new Category { Title="Brake Fluid", ParentCategoryId = 1, TypeHint = "product"},
            // new Category { Title="Coolant", ParentCategoryId = 1, TypeHint = "product"},

            // new Category { Title="Engine Parts", ParentCategoryId = 2, TypeHint = "product"},
            // new Category { Title="Suspension Parts", ParentCategoryId = 2, TypeHint = "product"},
            // new Category { Title="Body Parts", ParentCategoryId = 2, TypeHint = "product"},
            // new Category { Title="Electrical Parts", ParentCategoryId = 2, TypeHint = "product"},

            };

            return cats;
        }
        // private static List<Category> CreateSubCategories()
        // {
        //     var lub = _context.Categories.Single(x=>x.Title == "Lubricant").Id;
        //     var SP = _context.Categories.Single(x=>x.Title == "Spare Part").Id;


        //     var cats = new List<Category>{

        //     new Category { Title="Engine Oil", ParentCategoryId = lub, TypeHint = "product"},
        //     new Category { Title="Transmission Fluid", ParentCategoryId = lub, TypeHint = "product"},
        //     new Category { Title="Gear Oil", ParentCategoryId = lub, TypeHint = "product"},
        //     new Category { Title="Brake Fluid", ParentCategoryId = lub, TypeHint = "product"},
        //     new Category { Title="Coolant", ParentCategoryId = lub, TypeHint = "product"},

        //     new Category { Title="Engine Parts", ParentCategoryId = SP, TypeHint = "product"},
        //     new Category { Title="Suspension Parts", ParentCategoryId = SP, TypeHint = "product"},
        //     new Category { Title="Body Parts", ParentCategoryId = SP, TypeHint = "product"},
        //     new Category { Title="Electrical Parts", ParentCategoryId = SP, TypeHint = "product"},

        //     };

        //     return cats;
        // }

    }
}