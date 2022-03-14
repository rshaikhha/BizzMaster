using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace API.Data.DataInitializers
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

            context.UsageTypes.AddRange(createUsageType());
            context.SaveChanges();

            context.MasterSystems.AddRange(createMasterSystem());
            context.SaveChanges();

            var cats = CreateCategories();
            context.Categories.AddRange(cats);
            context.SaveChanges();

            var prods = createProducts();
            context.Products.AddRange(prods);
            context.SaveChanges();

            var supps = createSuppliers();
            context.Suppliers.AddRange(supps);
            context.SaveChanges();

            var slines = createSupplyLines();
            context.SupplyLines.AddRange(slines);
            context.SaveChanges();

            var lts = createLeadTimes();
            context.LeadTimes.AddRange(lts);
            context.SaveChanges();

            var cards = createCards();
            context.CommercialCards.AddRange(cards);
            context.SaveChanges();

            var regs = createRegistration();
            context.OrderRegistrations.AddRange(regs);
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
                new Brand { Title="Toyota", CountryId = jp, TypeHint = "#car,#product"},
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


        private static List<UsageType> createUsageType()
        {
            return new List<UsageType>
            {
                new UsageType { Title = "AutoService"},
                new UsageType { Title = "Mechanic"},
                new UsageType { Title = "Electrician"},
                new UsageType { Title = "Gas Station"},
                new UsageType { Title = "Store"},
            };

        }
        private static List<MasterSystem> createMasterSystem()
        {
            return new List<MasterSystem>
            {
                new MasterSystem { Title = "Engine"},
                new MasterSystem { Title = "Suspension"},
                new MasterSystem { Title = "Cabin"},
                new MasterSystem { Title = "Electric"},
            };

        }
        private static List<Category> CreateCategories()
        {

            var utas = _context.UsageTypes.FirstOrDefault(x => x.Title == "AutoService").Id;
            var utm = _context.UsageTypes.FirstOrDefault(x => x.Title == "Mechanic").Id;
            var ute = _context.UsageTypes.FirstOrDefault(x => x.Title == "Electrician").Id;

            var mse = _context.MasterSystems.FirstOrDefault(x => x.Title == "Engine").Id;
            var mss = _context.MasterSystems.FirstOrDefault(x => x.Title == "Suspension").Id;
            var msc = _context.MasterSystems.FirstOrDefault(x => x.Title == "Cabin").Id;



            var cats = new List<Category>{

            new Category { Title="Lubricant", Code = "10", Level = 0, ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas,
                Children = new List<Category>{
                    new Category { Title="Engine Oil", Code = "1001", Level = 1, ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mse, HSCode = "27101910"},
                    new Category { Title="Transmission Fluid", Code = "1002", Level = 1,ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mss, HSCode = "38190090"},
                    new Category { Title="Gear Oil", Code = "1003", Level = 1, ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mss, HSCode = "38190090" },
                    new Category { Title="Brake Fluid", Code = "1004", Level = 1, ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mss, HSCode = "38190090"},
                    new Category { Title="Coolant", Code = "1005", Level = 1, ItemUnit="Gallon", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mse, HSCode = "38190090"},
            }},

            new Category { Title="Spare Part", Code = "20", Level = 0,
                Children = new List<Category>{
                    new Category { Title="Engine Parts", Code="2001", Level = 1, ItemUnit="Piece", SetUnit="Set", UsageTypeId = utm, MasterSystemId = mse,
                        Children = new List<Category>{
                            new Category {Title = "Oil Filter", Code="200101", Level = 2, ItemUnit="Piece", SetUnit="Carton", UsageTypeId = utas, MasterSystemId = mse, HSCode = "84099190" },
                            new Category {Title = "Spark Plug", Code="200102", Level = 2, ItemUnit="Piece", SetUnit="Set", UsageTypeId = utas, MasterSystemId = mse, HSCode = "84099190", }
                        }
                    },
                    new Category { Title="Suspension Parts", Code="2002", Level = 1, ItemUnit="Piece", SetUnit="Set", UsageTypeId = utm, MasterSystemId = mss,
                        Children = new List<Category>{
                            new Category {Title = "Brake Pad", Code = "200201", Level = 2, ItemUnit = "Piece", SetUnit="Set", UsageTypeId = utm, MasterSystemId = mss, HSCode = "84099190"},
                            new Category {Title = "CV Joint",Code = "200202", Level = 2, ItemUnit = "Piece", SetUnit="Set", UsageTypeId = utm, MasterSystemId = mss, HSCode = "84099190"}
                        }
                    },
                    new Category { Title="Body Parts", Code = "2003", Level = 1, },
                    new Category { Title="Electrical Parts", Code = "2004", Level = 1, },
            }
            }
            };

            return cats;
        }

        private static List<Product> createProducts()
        {

            var aisin = _context.Brands.FirstOrDefault(x => x.Title == "AISIN").Id;
            var wolver = _context.Brands.FirstOrDefault(x => x.Title == "WOLVER").Id;
            var toyota = _context.Brands.FirstOrDefault(x => x.Title == "Toyota").Id;

            var E = _context.Categories.FirstOrDefault(x => x.Code == "1001").Id;
            var T = _context.Categories.FirstOrDefault(x => x.Code == "1002").Id;
            var C = _context.Categories.FirstOrDefault(x => x.Code == "1005").Id;
            var B = _context.Categories.FirstOrDefault(x => x.Code == "1004").Id;
            var BP = _context.Categories.FirstOrDefault(x => x.Code == "200201").Id;

            var ps = new List<Product>{
                new Product{ Title ="AISIN Engine Oil 10W-40 4L", PartNumber = "ESSN1044P", Description = "10W-40 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =1},
                new Product{ Title ="AISIN Engine Oil 10W-40 1L ", PartNumber = "ESSN1041P", Description = "10W-40 1L ",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =2},
                new Product{ Title ="AISIN Engine Oil 10W-40 3L", PartNumber = "ESSN1043P", Description = "10W-40 3L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 3, ItemWeight = 3, ItemPerSet = 4, Order =3},
                new Product{ Title ="AISIN ATF AFW PLUS 4L", PartNumber = "ATFMT4S", Description = "AFW PLUS 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =4},
                new Product{ Title ="AISIN ATF AFW PLUS 1L", PartNumber = "ATFMT1P", Description = "AFW PLUS 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =5},
                new Product{ Title ="AISIN ATF CVTF MULTI 4L", PartNumber = "CVTF004S", Description = "CVTF MULTI 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =6},
                new Product{ Title ="AISIN ATF CVTF MULTI 1L", PartNumber = "CVTF001P", Description = "CVTF MULTI 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =7},
                new Product{ Title ="AISIN ATF AFW-VI DEXRON-VI 4L", PartNumber = "ATFDVI4S", Description = "AFW-VI DEXRON-VI 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =8},
                new Product{ Title ="AISIN ATF AFW-VI DEXRON-VI 1L", PartNumber = "ATFDVI1P", Description = "AFW-VI DEXRON-VI 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =9},
                new Product{ Title ="AISIN ATF DEXRON III 4L", PartNumber = "ATFD34S", Description = "DEXRON III 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =10},
                new Product{ Title ="AISIN ATF DEXRON III 1L", PartNumber = "ATFD31P", Description = "DEXRON III 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =11},
                new Product{ Title ="AISIN Engine Oil 5W-30 5L", PartNumber = "ESFN0535P", Description = "5W-30 5L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 5, ItemWeight = 5, ItemPerSet = 3, Order =12},
                new Product{ Title ="AISIN Engine Oil 5W-30 4L ", PartNumber = "ESFN0534P", Description = "5W-30 4L ",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =13},
                new Product{ Title ="AISIN Engine Oil 5W-30 1L", PartNumber = "ESFN0531P", Description = "5W-30 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =14},
                new Product{ Title ="AISIN Engine Oil 5W-40 4L ", PartNumber = "ESFN0544P", Description = "5W-40 4L ",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =15},
                new Product{ Title ="AISIN Engine Oil 5W-40 1L", PartNumber = "ESFN0541P", Description = "5W-40 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =16},
                new Product{ Title ="AISIN Engine Oil 0W-20 4L", PartNumber = "ESFN0024P", Description = "0W-20 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =17},
                new Product{ Title ="AISIN Engine Oil 0W-20 1L", PartNumber = "ESFN0021P", Description = "0W-20 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =18},
                new Product{ Title ="AISIN Engine Oil 20W-50 4L ", PartNumber = "ESSN2054P", Description = "20W-50 4L ",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =19},
                new Product{ Title ="AISIN Engine Oil 20W-50 1L", PartNumber = "ESSN2051P", Description = "20W-50 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =20},
                new Product{ Title ="AISIN Gear Oil GL 75W-90 1L", PartNumber = "GSL57591P", Description = "GL 75W-90 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =21},
                new Product{ Title ="AISIN Gear Oil GL 80W-90 4L", PartNumber = "GSL58094P", Description = "GL 80W-90 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =22},
                new Product{ Title ="AISIN Gear Oil GL 80W-90 1L", PartNumber = "GSL58091P", Description = "GL 80W-90 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =23},
                new Product{ Title ="AISIN Gear Oil GL 85W-140 4L", PartNumber = "GSL585144P", Description = "GL 85W-140 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =24},
                new Product{ Title ="AISIN Gear Oil GL 80W-90 LSD 4L", PartNumber = "GSL5S8094P", Description = "GL 80W-90 LSD 4L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =25},
                new Product{ Title ="AISIN Engine Oil 10W-30 4L", PartNumber = "ESSN1034P", Description = "10W-30 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =26},
                new Product{ Title ="AISIN Engine Oil 10W-30 1L", PartNumber = "ESSN1031P", Description = "10W-30 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =27},
                new Product{ Title ="AISIN Engine Oil 0W-40 4L", PartNumber = "ESEN0044P", Description = "0W-40 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =28},
                new Product{ Title ="AISIN Engine Oil 0W-40 1L", PartNumber = "ESEN0041P", Description = "0W-40 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =29},
                new Product{ Title ="AISIN Brake Fluid DOT 3 0.5L", PartNumber = "BFSD3500G", Description = "DOT 3 0.5L",  BrandId = aisin ,  CategoryId =B,ItemVolume = 0.5, ItemWeight = 0.5, ItemPerSet = 24, Order =30},
                new Product{ Title ="AISIN Brake Fluid DOT 4 0.5L", PartNumber = "BFSD4500G", Description = "DOT 4 0.5L",  BrandId = aisin ,  CategoryId =B,ItemVolume = 0.5, ItemWeight = 0.5, ItemPerSet = 24, Order =31},
                new Product{ Title ="AISIN Coolant COOLANT RED 4L", PartNumber = "LCPM50A4LR", Description = "COOLANT RED 4L",  BrandId = aisin ,  CategoryId =C,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 6, Order =32},
                new Product{ Title ="AISIN Coolant COOLANT RED 1L", PartNumber = "LCPM50A1LR", Description = "COOLANT RED 1L",  BrandId = aisin ,  CategoryId =C,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 24, Order =33},
                new Product{ Title ="AISIN Coolant COOLANT GREEN 4L", PartNumber = "LCPM50A4LG", Description = "COOLANT GREEN 4L",  BrandId = aisin ,  CategoryId =C,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 6, Order =34},
                new Product{ Title ="AISIN Coolant COOLANT GREEN 1L", PartNumber = "LCPM50A1LG", Description = "COOLANT GREEN 1L",  BrandId = aisin ,  CategoryId =C,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 24, Order =35},
                new Product{ Title ="AISIN Engine Oil 0W-30 4L", PartNumber = "ESEN0034P", Description = "0W-30 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =36},
                new Product{ Title ="AISIN Engine Oil 0W-30 1L", PartNumber = "ESEN0031P", Description = "0W-30 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =37},
                new Product{ Title ="AISIN Engine Oil 5W-20 4L", PartNumber = "ESFN0524P", Description = "5W-20 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =38},
                new Product{ Title ="AISIN Engine Oil 5W-20 1L", PartNumber = "ESFN0521P", Description = "5W-20 1L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =39},
                new Product{ Title ="AISIN Engine Oil 20W-50 CF4 4L", PartNumber = "ECSF2054P", Description = "20W-50 CF4 4L",  BrandId = aisin ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =40},
                new Product{ Title ="AISIN ATF DCT 1L", PartNumber = "ATFDCT1P", Description = "DCT 1L",  BrandId = aisin ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 12, Order =41},
                new Product{ Title ="WOLVER ATF CVT SperFluid 1L", PartNumber = "WTATFCVT1S", Description = "W-CVT-SperFluid-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =42},
                new Product{ Title ="WOLVER ATF ATF6000 SuperFluid 1L", PartNumber = "WTATF601S", Description = "W-ATF6000-SuperFluid-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =43},
                new Product{ Title ="WOLVER ATF ATF WS 1L", PartNumber = "WATFWS1S", Description = "W-ATF WS-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =44},
                new Product{ Title ="WOLVER ATF ATF WS 4L", PartNumber = "WATFWS4S", Description = "W-ATF-WS-4L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =45},
                new Product{ Title ="WOLVER ATF ATF IV 1L", PartNumber = "WATFIV1S", Description = "W-ATF-IV-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =46},
                new Product{ Title ="WOLVER ATF ATF AL4 1L", PartNumber = "WATFAL41S", Description = "W-ATF-AL4-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =47},
                new Product{ Title ="WOLVER ATF ATF AL4 4L", PartNumber = "WATFAL44S", Description = "W-ATF-AL4-4L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =48},
                new Product{ Title ="WOLVER Gear Oil 75W90 MultiPurpose 1L", PartNumber = "WTMP7591S", Description = "W-75W90-MultiPurpose-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =49},
                new Product{ Title ="WOLVER Gear Oil 80w90 MultiGrade 1L", PartNumber = "WTMG8091S", Description = "W-80w90-MultiGrade-1L",  BrandId = wolver ,  CategoryId =T,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =50},
                new Product{ Title ="WOLVER Engine Oil 10W40 SuperDynamic 1L (SL)", PartNumber = "WESD1041S", Description = "W-10W40-SuperDynamic-1L (SL)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =51},
                new Product{ Title ="WOLVER Engine Oil 10W40 SuperDynamic 4L (SL)", PartNumber = "WESD1044S", Description = "W-10W40-SuperDynamic-4L (SL)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =52},
                new Product{ Title ="WOLVER Engine Oil 10W40 SuperDynamic 5L (SL)", PartNumber = "WESD1045S", Description = "W-10W40-SuperDynamic-5L (SL)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 5, ItemWeight = 5, ItemPerSet = 4, Order =53},
                new Product{ Title ="WOLVER Engine Oil 10W40 SuperLight 1L (SN)", PartNumber = "WESL1041S", Description = "W-10W40-SuperLight-1L (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =54},
                new Product{ Title ="WOLVER Engine Oil 10W40 SuperLight 4L (SN)", PartNumber = "WESL1044S", Description = "W-10W40-SuperLight-4L (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =55},
                new Product{ Title ="WOLVER Engine Oil 5W40 SuperTec 1L (SN)", PartNumber = "WEST0541S", Description = "W-5W40-SuperTec-1L (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =56},
                new Product{ Title ="WOLVER Engine Oil 5W40 SuperTec 4L  (SN)", PartNumber = "WEST0544S", Description = "W-5W40-SuperTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =57},
                new Product{ Title ="WOLVER Engine Oil 5W40 UltraTec 1L  (SN)", PartNumber = "WEUT0541S", Description = "W-5W40-UltraTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =58},
                new Product{ Title ="WOLVER Engine Oil 5W40 UltraTec 4L  (SN)", PartNumber = "WEUT0544S", Description = "W-5W40-UltraTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =59},
                new Product{ Title ="WOLVER Engine Oil 5W30 SuperTec 1L  (SN)", PartNumber = "WEST0531S", Description = "W-5W30-SuperTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =60},
                new Product{ Title ="WOLVER Engine Oil 5W30 SuperTec 4L  (SN)", PartNumber = "WEST0534S", Description = "W-5W30-SuperTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =61},
                new Product{ Title ="WOLVER Engine Oil 5W30 UltraTec 1L  (SN)", PartNumber = "WEUT0531S", Description = "W-5W30-UltraTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =62},
                new Product{ Title ="WOLVER Engine Oil 5W30 UltraTec 4L  (SN)", PartNumber = "WEUT0534S", Description = "W-5W30-UltraTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =63},
                new Product{ Title ="WOLVER Engine Oil 0W20 HighTec 1L  (SN)", PartNumber = "WEHT0021S", Description = "W-0W20-HighTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =64},
                new Product{ Title ="WOLVER Engine Oil 0W20 HighTec 4L  (SN)", PartNumber = "WEHT0024S", Description = "W-0W20-HighTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =65},
                new Product{ Title ="WOLVER Engine Oil 0W30 ProTec 1L  (SN)", PartNumber = "WEPT0031S", Description = "W-0W30-ProTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =66},
                new Product{ Title ="WOLVER Engine Oil 0W30 ProTec 4L  (SN)", PartNumber = "WEPT0034S", Description = "W-0W30-ProTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =67},
                new Product{ Title ="WOLVER Engine Oil 0W40 HighTec 1L  (SN)", PartNumber = "WEHT0041S", Description = "W-0W40-HighTec-1L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 1, ItemWeight = 1, ItemPerSet = 6, Order =68},
                new Product{ Title ="WOLVER Engine Oil 0W40 HighTec 4L  (SN)", PartNumber = "WEHT0044S", Description = "W-0W40-HighTec-4L  (SN)",  BrandId = wolver ,  CategoryId =E,ItemVolume = 4, ItemWeight = 4, ItemPerSet = 4, Order =69},

                new Product{ PartNumber = "04465-06110", Title = "04465-06110", Description = "BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =100},
                new Product{ PartNumber = "04465-YZZE2", Title = "04465-YZZE2", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =101},
                new Product{ PartNumber = "04465-YZZE3", Title = "04465-YZZE3", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =102},
                new Product{ PartNumber = "04465-YZZE4", Title = "04465-YZZE4", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =103},
                new Product{ PartNumber = "04465-YZZE8", Title = "04465-YZZE8", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =104},
                new Product{ PartNumber = "04465-YZZE9", Title = "04465-YZZE9", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =105},
                new Product{ PartNumber = "04465-YZZF6", Title = "04465-YZZF6", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =106},
                new Product{ PartNumber = "04465-YZZF7", Title = "04465-YZZF7", Description = "BRAKE PAD KIT",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =107},
                new Product{ PartNumber = "04465-YZZQ9", Title = "04465-YZZQ9", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =108},
                new Product{ PartNumber = "04465-YZZR2", Title = "04465-YZZR2", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =109},
                new Product{ PartNumber = "04465-YZZR4", Title = "04465-YZZR4", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =110},
                new Product{ PartNumber = "04465-YZZR4", Title = "04465-YZZR4", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =111},
                new Product{ PartNumber = "04465-YZZR5", Title = "04465-YZZR5", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =112},
                new Product{ PartNumber = "04465-YZZR6", Title = "04465-YZZR6", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =113},
                new Product{ PartNumber = "04466-YZZQ1", Title = "04466-YZZQ1", Description = "MVP BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =114},
                new Product{ PartNumber = "47735-30011", Title = "47735-30011", Description = "PIN, DISC BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =115},
                new Product{ PartNumber = "47735-50010", Title = "47735-50010", Description = "PIN, DISC BRAKE PAD",  BrandId = toyota ,  CategoryId = BP , ItemVolume = 0 , ItemWeight = 0, ItemPerSet = 2, Order =116},

            };

            return ps;
        }
    
        private static List<Supplier> createSuppliers()
        {

            var uae = _context.Countries.FirstOrDefault(x => x.Abbr == "AE").Id;
            var de = _context.Countries.FirstOrDefault(x => x.Abbr == "DE").Id;
            var supps = new List<Supplier>
            {
                new Supplier {
                    Title = "NEWEAST",
                    FullTitle = "New East General Trading",
                    Website = "www.neweast.ae",
                    Address = "P.O BOX  41534 Dubai, UAE",
                    CountryId = uae,
                    Contacts = new List<Contact>{
                        new Contact {
                            Title = "Ahmed NewEast",
                            FirstName = "Ahmed Juma",
                            LastName = "Ahli",
                            Initials = "Mr.",
                            Email = "ahmed@Neweast.co",
                            Position = "CEO",
                            Mobile = "+971-504534443",
                            WhatsApp = "+971-048811195",
                            IsMale = true
                        },
                        new Contact {
                            Title = "Sima NewEast",
                            FirstName = "Sima",
                            LastName = "Abedi",
                            Initials = "Ms.",
                            Email = "Sima@Neweast.co",
                            Position = "Executive Secretary",
                            Mobile = "+971-564338724",
                            WhatsApp = "+971-48811195",
                            IsMale = false
                        }
                    }

                },
                new Supplier {
                    Title = "WOLVER",
                    FullTitle = "JV Yukoil LLC.",
                    Website = "www.yuko.eu",
                    Address = "-----",
                    CountryId = de,
                    Contacts = new List<Contact>{
                        new Contact {
                            Title = "Aleksey Khoroshun",
                            FirstName = "Aleksey",
                            LastName = "Khoroshun",
                            Initials = "Mr.",
                            Email = "horoshun@yukoil.com",
                            Position = "Key Account Manager",
                            Mobile = "+38 (050) 422-88-78",
                            WhatsApp = "+38 (050) 422-88-78",
                            IsMale = true
                        }
                    }

                },
            };

            return supps;
        }
    
        private static List<SupplyLine> createSupplyLines()
        {
            var ne = _context.Suppliers.FirstOrDefault(x=>x.Title == "NEWEAST").Id;
            var wo = _context.Suppliers.FirstOrDefault(x=>x.Title == "WOLVER").Id;

            var Project = new Project
            {

            };

            var slines = new List<SupplyLine> 
            {
                new SupplyLine 
                {
                    Title = "Aisin Lubricants",
                    SupplierId = ne,
                    defaultPlanningType = PlanningType.Forward,
                    Products = _context.Products.Where(x=>x.Brand.Title == "AISIN").ToList()

                },
                new SupplyLine 
                {
                    Title = "Wolver Lubricants",
                    SupplierId = wo,
                    defaultPlanningType = PlanningType.Forward,
                    Products = _context.Products.Where(x=>x.Brand.Title == "WOLVER").ToList()
                },
            };

            return slines;
        }

        private static List<LeadTime> createLeadTimes()
        {
            var aisin = _context.SupplyLines.FirstOrDefault(x=>x.Title == "Aisin Lubricants").Id;
            var lts = new List<LeadTime>
            {
                new LeadTime{
                    Title = string.Format("LeadTime-{0}", aisin),
                    SupplyLineId = aisin,
                    Items = new List<LeadTimeItem>{
                        new LeadTimeItem { Title = "Analyze Shipment", Duration = 15, Order = 1},
                        new LeadTimeItem { Title = "Request Mofa", Duration = 10, Order = 2},
                        new LeadTimeItem { Title = "Loading", Duration = 2, Order = 3},
                        new LeadTimeItem { Title = "Shipping", Duration = 15, Order = 4},
                        new LeadTimeItem { Title = "Customs Clearance", Duration = 15, Order = 5},
                        new LeadTimeItem { Title = "Ship to Warehouse", Duration = 3, Order = 6},
                    }
                }
            };

            return lts;

        }

        private static List<CommercialCard> createCards()
        {
            return new List<CommercialCard>{
                new CommercialCard{ 
                    Title = "ARSH",
                    Description = "Arsh Fidar Shayan",
                    IssueDate = new DateTime(2021,10,01),
                    ValidityDate = new DateTime(2022,10,01),
                    FirstName = "Leila",
                    LastName = "Khalesi"
                    },
                new CommercialCard{ 
                    Title = "ARSTC",
                    Description = "Alireza Shabankare Trading",
                    IssueDate = new DateTime(2021,10,01),
                    ValidityDate = new DateTime(2022,10,01),
                    FirstName = "Alireza",
                    LastName = "Shabankareh"
                    }
            };
        }

        private static List<OrderRegistration> createRegistration()
        {
            var arsh = _context.CommercialCards.First(x=>x.Title == "ARSH").Id;
            var arstc = _context.CommercialCards.First(x=>x.Title == "ARSTC").Id;
            
            var E = _context.Categories.FirstOrDefault(x => x.Code == "1001");
            var T = _context.Categories.FirstOrDefault(x => x.Code == "1002");
            var C = _context.Categories.FirstOrDefault(x => x.Code == "1005");
            var B = _context.Categories.FirstOrDefault(x => x.Code == "1004");

            return new List<OrderRegistration>
            {
                new OrderRegistration
                {
                    Title = "Engine Oil Registraion No 12345",
                    CommercialCardId = arsh,
                    DocumentNumber = "123456",
                    RegistrationNumber = "123456",
                    Currency = "AED",
                    Amount = 500000,
                    Unit = "Litr",
                    Quantity = 50000,
                    IssueDate = new DateTime(2021,10,01),
                    ValidityDate = new DateTime(2022,10,01),
                    OrderRegistrationStatus = OrderRegistrationStatus.registered,
                    Categories = new List<Category>{E}
                },
                new OrderRegistration
                {
                    Title = "Transmission Oil Registraion No 12345",
                    CommercialCardId = arsh,
                    DocumentNumber = "123456",
                    RegistrationNumber = "123456",
                    Currency = "AED",
                    Amount = 100000,
                    Unit = "Litr",
                    Quantity = 10000,
                    IssueDate = new DateTime(2021,10,01),
                    ValidityDate = new DateTime(2022,10,01),
                    OrderRegistrationStatus = OrderRegistrationStatus.registered,
                    Categories = new List<Category>{T, B, C}
                },

            };
        }

    }
}