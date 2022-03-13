using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AuditDto
    {
        public int SupplyLineId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<AuditItemDto> Items {get; set;} = new List<AuditItemDto>();

        public int TotalOpenningStock {get => Items.Sum(x=>x.OpenningStock);}
        public int TotalSales {get => Items.Sum(x=>x.Sales);}
        public int TotalOrder {get => Items.Sum(x=>x.Order);}
        public int TotalShortage {get => Items.Sum(x=>x.Shortage);}
    }


    public class AuditItemDto
    {
        public int ProductId { get; set; }
        public int OpenningStock { get; set; }
        public int Sales { get; set; }
        public int Order { get; set; }
        public int Shortage { get; set; }

        public string ProductTitle {get; set;}
        public string ProductPartNumber { get; set; }
        public string ProductBrand { get; set; }
    }
}