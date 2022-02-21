using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abbr { get; set; }
        public string FlagImageUrl { get; set; }

    }
}