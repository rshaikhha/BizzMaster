using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SupplierDto
    {
        public int Id {get; set;}
        public string title {get; set;}
        public string FullTitle { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public List<ContactDto> Contacts {get; set;}

    }
}