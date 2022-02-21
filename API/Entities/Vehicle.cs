using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public int VehicleBrandId { get; set; }
        public VehicleBrand VehicleBrand { get; set; }
        public string Platform { get; set; }
        public string Model { get; set; }
        public string Years { get; set; }

    }


    public class VehicleBrand
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Country Country { get; set; }
        public bool Active { get; set; }
    }
}