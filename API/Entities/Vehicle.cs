using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Vehicle : BaseEntity
    {
        public int VehicleBrandId { get; set; }
        public Brand Brand { get; set; }
        public string Platform { get; set; }
        public string Model { get; set; }
        public string Years { get; set; }

    }


    
}