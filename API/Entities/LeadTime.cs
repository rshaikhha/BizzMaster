using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class LeadTime : BaseEntity
    {
        public int SupplyLineId { get; set; }
        public SupplyLine SupplyLine { get; set; }
        public List<LeadTimeItem> Items {get; set;}
    }

    public class LeadTimeItem : BaseEntity
    {
        public int Duration { get; set; }

        public int Order { get; set; }
    }
}