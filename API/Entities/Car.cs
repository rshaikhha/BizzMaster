using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Car : BaseEntity
    {
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        
    }

    public class Platform : BaseEntity
    {
        public int BrandId { get; set; }
        public Brand Brand {get; set;}

    }

    


    
}