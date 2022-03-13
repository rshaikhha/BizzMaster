using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ShipmentDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ShipmentItemDto> Items {get; set;} = new List<ShipmentItemDto>();

        public int TotalQuantity {get => Items.Sum(x=>x.Quantity);}
    }

    public class ShipmentItemDto 
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public string ProductTitle {get; set;}
        public string ProductPartNumner { get; set; }
        public string ProductBrand { get; set; }
    }

    public class ShipmentUploadDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ShipmentItemDto> Items {get; set;}
    }
}