using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class SalesForecast : BaseEntity
    {
        public int SupplyLineId { get; set; }
        public SupplyLine SupplyLine { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public List<SalesForecastItem> Items {get; set;}

        

    }

    public class SalesForecastItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}