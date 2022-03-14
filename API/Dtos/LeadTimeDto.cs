using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class LeadTimeDto 
    {
        public int SupplyLineId { get; set; }
        public List<LeadTimeItemDto> Items {get; set;} = new List<LeadTimeItemDto>();

        public int totalDuration {get => Items.Sum(x=>x.Duration);}

        public DateTime CreatedOn {get; set;}
    }

    public class LeadTimeItemDto
    {
        public string Title { get; set; }
        public int Duration { get; set; }

        public int Order {get; set;}
    }
}