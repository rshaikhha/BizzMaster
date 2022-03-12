using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{

    public class SalesForecastDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<SalesForecastItemDto> Items {get; set;} = new List<SalesForecastItemDto>();

        public int TotalQuantity {get => Items.Sum(x=>x.Quantity);}
    }


    public class SalesForecastUploadDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<SalesForecastItemDto> Items {get; set;}
    }

    public class SalesForecastItemDto 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductTitle {get; set;}
        public string ProductPartNumner { get; set; }
        public string ProductBrand { get; set; }
    }
}