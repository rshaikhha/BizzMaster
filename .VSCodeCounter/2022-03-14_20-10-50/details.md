# Details

Date : 2022-03-14 20:10:50

Directory e:\BGGT XRM\BizzMaster\API

Total : 64 files,  7104 codes, 97 comments, 1750 blanks, all 8951 lines

[summary](results.md) / details / [diff summary](diff.md) / [diff details](diff-details.md)

## Files
| filename | language | code | comment | blank | total |
| :--- | :--- | ---: | ---: | ---: | ---: |
| [API/API.csproj](/API/API.csproj) | XML | 22 | 0 | 4 | 26 |
| [API/Controllers/AccountController.cs](/API/Controllers/AccountController.cs) | C# | 76 | 0 | 15 | 91 |
| [API/Controllers/AuditController.cs](/API/Controllers/AuditController.cs) | C# | 73 | 0 | 18 | 91 |
| [API/Controllers/BaseApiController.cs](/API/Controllers/BaseApiController.cs) | C# | 13 | 0 | 2 | 15 |
| [API/Controllers/BasicsController.cs](/API/Controllers/BasicsController.cs) | C# | 77 | 2 | 15 | 94 |
| [API/Controllers/CarController.cs](/API/Controllers/CarController.cs) | C# | 112 | 0 | 30 | 142 |
| [API/Controllers/CommercialController.cs](/API/Controllers/CommercialController.cs) | C# | 106 | 0 | 17 | 123 |
| [API/Controllers/OrderController.cs](/API/Controllers/OrderController.cs) | C# | 183 | 21 | 55 | 259 |
| [API/Controllers/ProductsController.cs](/API/Controllers/ProductsController.cs) | C# | 78 | 0 | 21 | 99 |
| [API/Controllers/ProjectController.cs](/API/Controllers/ProjectController.cs) | C# | 118 | 2 | 19 | 139 |
| [API/Controllers/SalesForecastController.cs](/API/Controllers/SalesForecastController.cs) | C# | 145 | 21 | 45 | 211 |
| [API/Controllers/ShipmentController.cs](/API/Controllers/ShipmentController.cs) | C# | 160 | 0 | 42 | 202 |
| [API/Controllers/StockController.cs](/API/Controllers/StockController.cs) | C# | 197 | 21 | 59 | 277 |
| [API/Controllers/SuppliersController.cs](/API/Controllers/SuppliersController.cs) | C# | 218 | 23 | 70 | 311 |
| [API/Data/BMContext.cs](/API/Data/BMContext.cs) | C# | 52 | 0 | 17 | 69 |
| [API/Data/BizzMaster.db](/API/Data/BizzMaster.db) | Database | 713 | 0 | 35 | 748 |
| [API/Data/DataInitializers/DbInitializer.cs](/API/Data/DataInitializers/DbInitializer.cs) | C# | 529 | 0 | 91 | 620 |
| [API/Data/Migrations/20220314110433_InitialCreate.Designer.cs](/API/Data/Migrations/20220314110433_InitialCreate.Designer.cs) | C# | 1,036 | 1 | 404 | 1,441 |
| [API/Data/Migrations/20220314110433_InitialCreate.cs](/API/Data/Migrations/20220314110433_InitialCreate.cs) | C# | 1,001 | 0 | 104 | 1,105 |
| [API/Data/Migrations/BMContextModelSnapshot.cs](/API/Data/Migrations/BMContextModelSnapshot.cs) | C# | 1,034 | 1 | 404 | 1,439 |
| [API/Dtos/AuditDto.cs](/API/Dtos/AuditDto.cs) | C# | 29 | 0 | 5 | 34 |
| [API/Dtos/BrandDto.cs](/API/Dtos/BrandDto.cs) | C# | 16 | 0 | 3 | 19 |
| [API/Dtos/CarDtos.cs](/API/Dtos/CarDtos.cs) | C# | 28 | 0 | 3 | 31 |
| [API/Dtos/CategoryDto.cs](/API/Dtos/CategoryDto.cs) | C# | 21 | 0 | 4 | 25 |
| [API/Dtos/CommercialDto.cs](/API/Dtos/CommercialDto.cs) | C# | 42 | 0 | 21 | 63 |
| [API/Dtos/ContactDto.cs](/API/Dtos/ContactDto.cs) | C# | 19 | 0 | 1 | 20 |
| [API/Dtos/LeadTimeDto.cs](/API/Dtos/LeadTimeDto.cs) | C# | 20 | 0 | 5 | 25 |
| [API/Dtos/LoginDto.cs](/API/Dtos/LoginDto.cs) | C# | 12 | 0 | 1 | 13 |
| [API/Dtos/OrderDto.cs](/API/Dtos/OrderDto.cs) | C# | 31 | 0 | 5 | 36 |
| [API/Dtos/ProductDto.cs](/API/Dtos/ProductDto.cs) | C# | 20 | 0 | 2 | 22 |
| [API/Dtos/ProjectDto.cs](/API/Dtos/ProjectDto.cs) | C# | 75 | 0 | 28 | 103 |
| [API/Dtos/RegisterDto.cs](/API/Dtos/RegisterDto.cs) | C# | 11 | 0 | 1 | 12 |
| [API/Dtos/SalesForecastDto.cs](/API/Dtos/SalesForecastDto.cs) | C# | 30 | 0 | 9 | 39 |
| [API/Dtos/ShipmentDto.cs](/API/Dtos/ShipmentDto.cs) | C# | 30 | 0 | 5 | 35 |
| [API/Dtos/StockDto.cs](/API/Dtos/StockDto.cs) | C# | 30 | 0 | 5 | 35 |
| [API/Dtos/SupplierDto.cs](/API/Dtos/SupplierDto.cs) | C# | 18 | 0 | 2 | 20 |
| [API/Dtos/SupplyLineDto.cs](/API/Dtos/SupplyLineDto.cs) | C# | 15 | 0 | 3 | 18 |
| [API/Dtos/UserDto.cs](/API/Dtos/UserDto.cs) | C# | 13 | 0 | 1 | 14 |
| [API/Entities/BaseEntity.cs](/API/Entities/BaseEntity.cs) | C# | 21 | 0 | 11 | 32 |
| [API/Entities/Basics.cs](/API/Entities/Basics.cs) | C# | 45 | 0 | 16 | 61 |
| [API/Entities/Car.cs](/API/Entities/Car.cs) | C# | 18 | 0 | 9 | 27 |
| [API/Entities/LeadTime.cs](/API/Entities/LeadTime.cs) | C# | 18 | 0 | 3 | 21 |
| [API/Entities/Order.cs](/API/Entities/Order.cs) | C# | 21 | 0 | 2 | 23 |
| [API/Entities/OrderRegistration.cs](/API/Entities/OrderRegistration.cs) | C# | 36 | 0 | 19 | 55 |
| [API/Entities/Product.cs](/API/Entities/Product.cs) | C# | 17 | 0 | 16 | 33 |
| [API/Entities/Project.cs](/API/Entities/Project.cs) | C# | 35 | 0 | 11 | 46 |
| [API/Entities/SalesForecast.cs](/API/Entities/SalesForecast.cs) | C# | 21 | 2 | 3 | 26 |
| [API/Entities/Shipment.cs](/API/Entities/Shipment.cs) | C# | 21 | 0 | 2 | 23 |
| [API/Entities/Stock.cs](/API/Entities/Stock.cs) | C# | 21 | 0 | 3 | 24 |
| [API/Entities/Supply.cs](/API/Entities/Supply.cs) | C# | 30 | 0 | 13 | 43 |
| [API/Entities/User.cs](/API/Entities/User.cs) | C# | 11 | 0 | 2 | 13 |
| [API/Extensions/HttpExtensions.cs](/API/Extensions/HttpExtensions.cs) | C# | 19 | 0 | 3 | 22 |
| [API/Extensions/ProductExtensions.cs](/API/Extensions/ProductExtensions.cs) | C# | 41 | 0 | 14 | 55 |
| [API/Program.cs](/API/Program.cs) | C# | 44 | 0 | 12 | 56 |
| [API/Properties/launchSettings.json](/API/Properties/launchSettings.json) | JSON | 31 | 0 | 1 | 32 |
| [API/RequestHelpers/MetaData.cs](/API/RequestHelpers/MetaData.cs) | C# | 14 | 0 | 1 | 15 |
| [API/RequestHelpers/PagedList.cs](/API/RequestHelpers/PagedList.cs) | C# | 32 | 0 | 7 | 39 |
| [API/RequestHelpers/PaginationParams.cs](/API/RequestHelpers/PaginationParams.cs) | C# | 18 | 0 | 1 | 19 |
| [API/RequestHelpers/ProductParams.cs](/API/RequestHelpers/ProductParams.cs) | C# | 14 | 0 | 2 | 16 |
| [API/Services/TokenService.cs](/API/Services/TokenService.cs) | C# | 48 | 0 | 7 | 55 |
| [API/Startup.cs](/API/Startup.cs) | C# | 89 | 3 | 14 | 106 |
| [API/WeatherForecast.cs](/API/WeatherForecast.cs) | C# | 11 | 0 | 5 | 16 |
| [API/appsettings.Development.json](/API/appsettings.Development.json) | JSON | 15 | 0 | 1 | 16 |
| [API/appsettings.json](/API/appsettings.json) | JSON | 10 | 0 | 1 | 11 |

[summary](results.md) / details / [diff summary](diff.md) / [diff details](diff-details.md)