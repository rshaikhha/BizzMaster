
using System.Collections.Generic;

namespace API.Entities
{
    public class Product : BaseEntity
    {
        public string PartNumber { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public double ItemVolume { get; set; }
        public double ItemWeight { get; set; }
        public int ItemPerSet { get; set; }
        public int Order { get; set; }


    }

    

    
    
     



}