using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SupplyLineDto
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string Supplier {get; set;}

        public List<ProductDto> Products {get; set;}
        
        public string defaultPlanningType {get; set;}
    }
}