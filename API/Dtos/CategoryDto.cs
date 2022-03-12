using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace API.Dtos
{
    public class CategoryDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public List<CategoryDto> Children { get; set; }
        public bool hasChildren { get { return this.Children != null && this.Children.Count > 0; } }

        public string ItemUnit { get; set; }
        public string SetUnit { get; set; }
        public string UsageType { get; set; }
        public string MasterSystem { get; set; }
        public string HSCode { get; set; }

    }
}