using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Supplier : BaseEntity
    {

        public int CountryId {get; set;}
        public Country Country { get; set; }
        public List<Contact> Contacts {get; set;}
        
    }
}