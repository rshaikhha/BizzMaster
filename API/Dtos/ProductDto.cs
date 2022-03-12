using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public double ItemVolume { get; set; }
        public double ItemWeight { get; set; }
        public int ItemPerSet { get; set; }
        public int Order { get; set; }
    }
}