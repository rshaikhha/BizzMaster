# Diff Details

Date : 2022-03-14 20:11:02

Directory e:\BGGT XRM\BizzMaster\client\src

Total : 137 files,  -2435 codes, 127 comments, -787 blanks, all -3095 lines

[summary](results.md) / [details](details.md) / [diff summary](diff.md) / diff details

## Files
| filename | language | code | comment | blank | total |
| :--- | :--- | ---: | ---: | ---: | ---: |
| [API/API.csproj](/API/API.csproj) | XML | -22 | 0 | -4 | -26 |
| [API/Controllers/AccountController.cs](/API/Controllers/AccountController.cs) | C# | -76 | 0 | -15 | -91 |
| [API/Controllers/AuditController.cs](/API/Controllers/AuditController.cs) | C# | -73 | 0 | -18 | -91 |
| [API/Controllers/BaseApiController.cs](/API/Controllers/BaseApiController.cs) | C# | -13 | 0 | -2 | -15 |
| [API/Controllers/BasicsController.cs](/API/Controllers/BasicsController.cs) | C# | -77 | -2 | -15 | -94 |
| [API/Controllers/CarController.cs](/API/Controllers/CarController.cs) | C# | -112 | 0 | -30 | -142 |
| [API/Controllers/CommercialController.cs](/API/Controllers/CommercialController.cs) | C# | -106 | 0 | -17 | -123 |
| [API/Controllers/OrderController.cs](/API/Controllers/OrderController.cs) | C# | -183 | -21 | -55 | -259 |
| [API/Controllers/ProductsController.cs](/API/Controllers/ProductsController.cs) | C# | -78 | 0 | -21 | -99 |
| [API/Controllers/ProjectController.cs](/API/Controllers/ProjectController.cs) | C# | -118 | -2 | -19 | -139 |
| [API/Controllers/SalesForecastController.cs](/API/Controllers/SalesForecastController.cs) | C# | -145 | -21 | -45 | -211 |
| [API/Controllers/ShipmentController.cs](/API/Controllers/ShipmentController.cs) | C# | -160 | 0 | -42 | -202 |
| [API/Controllers/StockController.cs](/API/Controllers/StockController.cs) | C# | -197 | -21 | -59 | -277 |
| [API/Controllers/SuppliersController.cs](/API/Controllers/SuppliersController.cs) | C# | -218 | -23 | -70 | -311 |
| [API/Data/BMContext.cs](/API/Data/BMContext.cs) | C# | -52 | 0 | -17 | -69 |
| [API/Data/BizzMaster.db](/API/Data/BizzMaster.db) | Database | -713 | 0 | -35 | -748 |
| [API/Data/DataInitializers/DbInitializer.cs](/API/Data/DataInitializers/DbInitializer.cs) | C# | -529 | 0 | -91 | -620 |
| [API/Data/Migrations/20220314110433_InitialCreate.Designer.cs](/API/Data/Migrations/20220314110433_InitialCreate.Designer.cs) | C# | -1,036 | -1 | -404 | -1,441 |
| [API/Data/Migrations/20220314110433_InitialCreate.cs](/API/Data/Migrations/20220314110433_InitialCreate.cs) | C# | -1,001 | 0 | -104 | -1,105 |
| [API/Data/Migrations/BMContextModelSnapshot.cs](/API/Data/Migrations/BMContextModelSnapshot.cs) | C# | -1,034 | -1 | -404 | -1,439 |
| [API/Dtos/AuditDto.cs](/API/Dtos/AuditDto.cs) | C# | -29 | 0 | -5 | -34 |
| [API/Dtos/BrandDto.cs](/API/Dtos/BrandDto.cs) | C# | -16 | 0 | -3 | -19 |
| [API/Dtos/CarDtos.cs](/API/Dtos/CarDtos.cs) | C# | -28 | 0 | -3 | -31 |
| [API/Dtos/CategoryDto.cs](/API/Dtos/CategoryDto.cs) | C# | -21 | 0 | -4 | -25 |
| [API/Dtos/CommercialDto.cs](/API/Dtos/CommercialDto.cs) | C# | -42 | 0 | -21 | -63 |
| [API/Dtos/ContactDto.cs](/API/Dtos/ContactDto.cs) | C# | -19 | 0 | -1 | -20 |
| [API/Dtos/LeadTimeDto.cs](/API/Dtos/LeadTimeDto.cs) | C# | -20 | 0 | -5 | -25 |
| [API/Dtos/LoginDto.cs](/API/Dtos/LoginDto.cs) | C# | -12 | 0 | -1 | -13 |
| [API/Dtos/OrderDto.cs](/API/Dtos/OrderDto.cs) | C# | -31 | 0 | -5 | -36 |
| [API/Dtos/ProductDto.cs](/API/Dtos/ProductDto.cs) | C# | -20 | 0 | -2 | -22 |
| [API/Dtos/ProjectDto.cs](/API/Dtos/ProjectDto.cs) | C# | -75 | 0 | -28 | -103 |
| [API/Dtos/RegisterDto.cs](/API/Dtos/RegisterDto.cs) | C# | -11 | 0 | -1 | -12 |
| [API/Dtos/SalesForecastDto.cs](/API/Dtos/SalesForecastDto.cs) | C# | -30 | 0 | -9 | -39 |
| [API/Dtos/ShipmentDto.cs](/API/Dtos/ShipmentDto.cs) | C# | -30 | 0 | -5 | -35 |
| [API/Dtos/StockDto.cs](/API/Dtos/StockDto.cs) | C# | -30 | 0 | -5 | -35 |
| [API/Dtos/SupplierDto.cs](/API/Dtos/SupplierDto.cs) | C# | -18 | 0 | -2 | -20 |
| [API/Dtos/SupplyLineDto.cs](/API/Dtos/SupplyLineDto.cs) | C# | -15 | 0 | -3 | -18 |
| [API/Dtos/UserDto.cs](/API/Dtos/UserDto.cs) | C# | -13 | 0 | -1 | -14 |
| [API/Entities/BaseEntity.cs](/API/Entities/BaseEntity.cs) | C# | -21 | 0 | -11 | -32 |
| [API/Entities/Basics.cs](/API/Entities/Basics.cs) | C# | -45 | 0 | -16 | -61 |
| [API/Entities/Car.cs](/API/Entities/Car.cs) | C# | -18 | 0 | -9 | -27 |
| [API/Entities/LeadTime.cs](/API/Entities/LeadTime.cs) | C# | -18 | 0 | -3 | -21 |
| [API/Entities/Order.cs](/API/Entities/Order.cs) | C# | -21 | 0 | -2 | -23 |
| [API/Entities/OrderRegistration.cs](/API/Entities/OrderRegistration.cs) | C# | -36 | 0 | -19 | -55 |
| [API/Entities/Product.cs](/API/Entities/Product.cs) | C# | -17 | 0 | -16 | -33 |
| [API/Entities/Project.cs](/API/Entities/Project.cs) | C# | -35 | 0 | -11 | -46 |
| [API/Entities/SalesForecast.cs](/API/Entities/SalesForecast.cs) | C# | -21 | -2 | -3 | -26 |
| [API/Entities/Shipment.cs](/API/Entities/Shipment.cs) | C# | -21 | 0 | -2 | -23 |
| [API/Entities/Stock.cs](/API/Entities/Stock.cs) | C# | -21 | 0 | -3 | -24 |
| [API/Entities/Supply.cs](/API/Entities/Supply.cs) | C# | -30 | 0 | -13 | -43 |
| [API/Entities/User.cs](/API/Entities/User.cs) | C# | -11 | 0 | -2 | -13 |
| [API/Extensions/HttpExtensions.cs](/API/Extensions/HttpExtensions.cs) | C# | -19 | 0 | -3 | -22 |
| [API/Extensions/ProductExtensions.cs](/API/Extensions/ProductExtensions.cs) | C# | -41 | 0 | -14 | -55 |
| [API/Program.cs](/API/Program.cs) | C# | -44 | 0 | -12 | -56 |
| [API/Properties/launchSettings.json](/API/Properties/launchSettings.json) | JSON | -31 | 0 | -1 | -32 |
| [API/RequestHelpers/MetaData.cs](/API/RequestHelpers/MetaData.cs) | C# | -14 | 0 | -1 | -15 |
| [API/RequestHelpers/PagedList.cs](/API/RequestHelpers/PagedList.cs) | C# | -32 | 0 | -7 | -39 |
| [API/RequestHelpers/PaginationParams.cs](/API/RequestHelpers/PaginationParams.cs) | C# | -18 | 0 | -1 | -19 |
| [API/RequestHelpers/ProductParams.cs](/API/RequestHelpers/ProductParams.cs) | C# | -14 | 0 | -2 | -16 |
| [API/Services/TokenService.cs](/API/Services/TokenService.cs) | C# | -48 | 0 | -7 | -55 |
| [API/Startup.cs](/API/Startup.cs) | C# | -89 | -3 | -14 | -106 |
| [API/WeatherForecast.cs](/API/WeatherForecast.cs) | C# | -11 | 0 | -5 | -16 |
| [API/appsettings.Development.json](/API/appsettings.Development.json) | JSON | -15 | 0 | -1 | -16 |
| [API/appsettings.json](/API/appsettings.json) | JSON | -10 | 0 | -1 | -11 |
| [client/src/app/api/agent.ts](/client/src/app/api/agent.ts) | TypeScript | 153 | 0 | 26 | 179 |
| [client/src/app/components/AppPagination.tsx](/client/src/app/components/AppPagination.tsx) | TypeScript React | 27 | 0 | 4 | 31 |
| [client/src/app/components/CheckBoxButtons.tsx](/client/src/app/components/CheckBoxButtons.tsx) | TypeScript React | 33 | 0 | 5 | 38 |
| [client/src/app/components/RadioButtonGroup.tsx](/client/src/app/components/RadioButtonGroup.tsx) | TypeScript React | 29 | 0 | 5 | 34 |
| [client/src/app/layout/App.tsx](/client/src/app/layout/App.tsx) | TypeScript React | 133 | 0 | 25 | 158 |
| [client/src/app/layout/Header.tsx](/client/src/app/layout/Header.tsx) | TypeScript React | 81 | 5 | 12 | 98 |
| [client/src/app/layout/Loadingcomponent.tsx](/client/src/app/layout/Loadingcomponent.tsx) | TypeScript React | 15 | 0 | 2 | 17 |
| [client/src/app/layout/Sidebar.tsx](/client/src/app/layout/Sidebar.tsx) | TypeScript React | 86 | 0 | 13 | 99 |
| [client/src/app/layout/SignedInMenu.tsx](/client/src/app/layout/SignedInMenu.tsx) | TypeScript React | 41 | 0 | 3 | 44 |
| [client/src/app/layout/styles.css](/client/src/app/layout/styles.css) | CSS | 0 | 0 | 1 | 1 |
| [client/src/app/models/car.ts](/client/src/app/models/car.ts) | TypeScript | 29 | 0 | 2 | 31 |
| [client/src/app/models/pagination.ts](/client/src/app/models/pagination.ts) | TypeScript | 14 | 0 | 2 | 16 |
| [client/src/app/models/product.ts](/client/src/app/models/product.ts) | TypeScript | 21 | 0 | 4 | 25 |
| [client/src/app/models/supply.ts](/client/src/app/models/supply.ts) | TypeScript | 22 | 0 | 2 | 24 |
| [client/src/app/models/user.ts](/client/src/app/models/user.ts) | TypeScript | 5 | 0 | 0 | 5 |
| [client/src/app/store/configureStore.ts](/client/src/app/store/configureStore.ts) | TypeScript | 16 | 0 | 6 | 22 |
| [client/src/features/about/AboutPage.tsx](/client/src/features/about/AboutPage.tsx) | TypeScript React | 8 | 0 | 1 | 9 |
| [client/src/features/account/Register.tsx](/client/src/features/account/Register.tsx) | TypeScript React | 105 | 0 | 6 | 111 |
| [client/src/features/account/accountSlice.ts](/client/src/features/account/accountSlice.ts) | TypeScript | 70 | 0 | 9 | 79 |
| [client/src/features/account/login.tsx](/client/src/features/account/login.tsx) | TypeScript React | 74 | 0 | 14 | 88 |
| [client/src/features/cars/CarBrands.tsx](/client/src/features/cars/CarBrands.tsx) | TypeScript React | 18 | 0 | 11 | 29 |
| [client/src/features/cars/CarCard.tsx](/client/src/features/cars/CarCard.tsx) | TypeScript React | 75 | 0 | 8 | 83 |
| [client/src/features/cars/CarFinder.tsx](/client/src/features/cars/CarFinder.tsx) | TypeScript React | 64 | 0 | 17 | 81 |
| [client/src/features/cars/Cars-old.tsx](/client/src/features/cars/Cars-old.tsx) | TypeScript React | 98 | 29 | 27 | 154 |
| [client/src/features/cars/Cars.tsx](/client/src/features/cars/Cars.tsx) | TypeScript React | 20 | 0 | 11 | 31 |
| [client/src/features/cars/Platforms.tsx](/client/src/features/cars/Platforms.tsx) | TypeScript React | 19 | 0 | 11 | 30 |
| [client/src/features/commercial/CommercialCardDetail.tsx](/client/src/features/commercial/CommercialCardDetail.tsx) | TypeScript React | 50 | 0 | 12 | 62 |
| [client/src/features/commercial/CommercialCards.tsx](/client/src/features/commercial/CommercialCards.tsx) | TypeScript React | 34 | 0 | 19 | 53 |
| [client/src/features/commercial/OrderRegistrationDetail.tsx](/client/src/features/commercial/OrderRegistrationDetail.tsx) | TypeScript React | 57 | 0 | 12 | 69 |
| [client/src/features/commercial/OrderRegistrations.tsx](/client/src/features/commercial/OrderRegistrations.tsx) | TypeScript React | 35 | 0 | 19 | 54 |
| [client/src/features/error/NotFound.tsx](/client/src/features/error/NotFound.tsx) | TypeScript React | 14 | 0 | 1 | 15 |
| [client/src/features/home/HomePage.tsx](/client/src/features/home/HomePage.tsx) | TypeScript React | 8 | 0 | 1 | 9 |
| [client/src/features/leadtime/LeadTime.tsx](/client/src/features/leadtime/LeadTime.tsx) | TypeScript React | 65 | 0 | 17 | 82 |
| [client/src/features/leadtime/LeadTimeHistory.tsx](/client/src/features/leadtime/LeadTimeHistory.tsx) | TypeScript React | 68 | 1 | 13 | 82 |
| [client/src/features/leadtime/SubmitLeadTime.tsx](/client/src/features/leadtime/SubmitLeadTime.tsx) | TypeScript React | 193 | 3 | 24 | 220 |
| [client/src/features/order/Order.tsx](/client/src/features/order/Order.tsx) | TypeScript React | 81 | 0 | 19 | 100 |
| [client/src/features/order/OrderHistory.tsx](/client/src/features/order/OrderHistory.tsx) | TypeScript React | 79 | 1 | 17 | 97 |
| [client/src/features/order/SubmitOrder.tsx](/client/src/features/order/SubmitOrder.tsx) | TypeScript React | 258 | 23 | 32 | 313 |
| [client/src/features/product/ProductDetails.tsx](/client/src/features/product/ProductDetails.tsx) | TypeScript React | 56 | 42 | 12 | 110 |
| [client/src/features/product/ProductSearch.tsx](/client/src/features/product/ProductSearch.tsx) | TypeScript React | 25 | 0 | 6 | 31 |
| [client/src/features/product/Products.tsx](/client/src/features/product/Products.tsx) | TypeScript React | 87 | 0 | 19 | 106 |
| [client/src/features/product/catalogSlice.ts](/client/src/features/product/catalogSlice.ts) | TypeScript | 141 | 1 | 17 | 159 |
| [client/src/features/project/ProjectDetail.tsx](/client/src/features/project/ProjectDetail.tsx) | TypeScript React | 59 | 0 | 22 | 81 |
| [client/src/features/project/ProjectWizard1.tsx](/client/src/features/project/ProjectWizard1.tsx) | TypeScript React | 30 | 0 | 17 | 47 |
| [client/src/features/project/ProjectWizard2.tsx](/client/src/features/project/ProjectWizard2.tsx) | TypeScript React | 260 | 23 | 32 | 315 |
| [client/src/features/project/ProjectWizard3.tsx](/client/src/features/project/ProjectWizard3.tsx) | TypeScript React | 244 | 6 | 37 | 287 |
| [client/src/features/project/Projects.tsx](/client/src/features/project/Projects.tsx) | TypeScript React | 39 | 1 | 20 | 60 |
| [client/src/features/shared/HierarchialTable.tsx](/client/src/features/shared/HierarchialTable.tsx) | TypeScript React | 54 | 0 | 12 | 66 |
| [client/src/features/shared/SimpleMessage.tsx](/client/src/features/shared/SimpleMessage.tsx) | TypeScript React | 31 | 31 | 6 | 68 |
| [client/src/features/shared/SimpleTable.tsx](/client/src/features/shared/SimpleTable.tsx) | TypeScript React | 48 | 0 | 9 | 57 |
| [client/src/features/stock/Stock.tsx](/client/src/features/stock/Stock.tsx) | TypeScript React | 82 | 0 | 19 | 101 |
| [client/src/features/stock/StockHistory.tsx](/client/src/features/stock/StockHistory.tsx) | TypeScript React | 79 | 1 | 17 | 97 |
| [client/src/features/stock/SubmitStock.tsx](/client/src/features/stock/SubmitStock.tsx) | TypeScript React | 258 | 23 | 33 | 314 |
| [client/src/features/supply/SalesForecast.tsx](/client/src/features/supply/SalesForecast.tsx) | TypeScript React | 84 | 0 | 19 | 103 |
| [client/src/features/supply/SalesForecastHistory.tsx](/client/src/features/supply/SalesForecastHistory.tsx) | TypeScript React | 79 | 1 | 17 | 97 |
| [client/src/features/supply/SubmitSalesForecast.tsx](/client/src/features/supply/SubmitSalesForecast.tsx) | TypeScript React | 241 | 23 | 28 | 292 |
| [client/src/features/supply/SupplierDetails.tsx](/client/src/features/supply/SupplierDetails.tsx) | TypeScript React | 87 | 0 | 16 | 103 |
| [client/src/features/supply/Suppliers.tsx](/client/src/features/supply/Suppliers.tsx) | TypeScript React | 32 | 0 | 19 | 51 |
| [client/src/features/supply/SupplyLineAudit.tsx](/client/src/features/supply/SupplyLineAudit.tsx) | TypeScript React | 153 | 2 | 39 | 194 |
| [client/src/features/supply/SupplyLineDetails.tsx](/client/src/features/supply/SupplyLineDetails.tsx) | TypeScript React | 64 | 0 | 22 | 86 |
| [client/src/features/supply/SupplyLines.tsx](/client/src/features/supply/SupplyLines.tsx) | TypeScript React | 32 | 0 | 18 | 50 |
| [client/src/features/system/Basics.tsx](/client/src/features/system/Basics.tsx) | TypeScript React | 38 | 0 | 11 | 49 |
| [client/src/features/system/Brands.tsx](/client/src/features/system/Brands.tsx) | TypeScript React | 18 | 0 | 11 | 29 |
| [client/src/features/system/Categories.tsx](/client/src/features/system/Categories.tsx) | TypeScript React | 25 | 0 | 12 | 37 |
| [client/src/features/system/Countries.tsx](/client/src/features/system/Countries.tsx) | TypeScript React | 20 | 0 | 11 | 31 |
| [client/src/features/system/MasterSystems.tsx](/client/src/features/system/MasterSystems.tsx) | TypeScript React | 17 | 0 | 10 | 27 |
| [client/src/features/system/Usagetypes.tsx](/client/src/features/system/Usagetypes.tsx) | TypeScript React | 17 | 0 | 10 | 27 |
| [client/src/features/system/basicsSlice.ts](/client/src/features/system/basicsSlice.ts) | TypeScript | 29 | 0 | 16 | 45 |
| [client/src/index.tsx](/client/src/index.tsx) | TypeScript React | 20 | 3 | 6 | 29 |
| [client/src/react-app-env.d.ts](/client/src/react-app-env.d.ts) | TypeScript | 0 | 1 | 1 | 2 |
| [client/src/reportWebVitals.ts](/client/src/reportWebVitals.ts) | TypeScript | 13 | 0 | 3 | 16 |
| [client/src/test/App.test.tsx](/client/src/test/App.test.tsx) | TypeScript React | 8 | 0 | 2 | 10 |
| [client/src/test/setupTests.ts](/client/src/test/setupTests.ts) | TypeScript | 1 | 4 | 1 | 6 |

[summary](results.md) / [details](details.md) / [diff summary](diff.md) / diff details