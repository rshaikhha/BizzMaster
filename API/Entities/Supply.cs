using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Supplier : BaseEntity
    {
        public string FullTitle { get; set; }
        public int CountryId {get; set;}
        public Country Country { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public List<Contact> Contacts {get; set;}
        
    }

    public enum PlanningType {
        Forward = 1,
        Average = 2,
        Manual = 3,
    }

    public class SupplyLine : BaseEntity
    {
        public int SupplierId {get; set;}
        public Supplier Supplier {get; set;}

        public IList<Product> Products {get; set;} = new List<Product>();
        
        public PlanningType defaultPlanningType {get; set;} = PlanningType.Manual;
    }

    
}