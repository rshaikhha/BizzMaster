using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Shipment : BaseEntity
    {
        public int SupplyLineId { get; set; }
        public SupplyLine SupplyLine { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public List<ShipmentItem> Items {get; set;}
    }

    public class ShipmentItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}