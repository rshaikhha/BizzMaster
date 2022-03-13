using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class StockDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<StockItemDto> Items {get; set;} = new List<StockItemDto>();

        public int TotalQuantity {get => Items.Sum(x=>x.Quantity);}
    }

    public class StockItemDto 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductTitle {get; set;}
        public string ProductPartNumner { get; set; }
        public string ProductBrand { get; set; }
    }

    public class StockUploadDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<StockItemDto> Items {get; set;}
    }
}