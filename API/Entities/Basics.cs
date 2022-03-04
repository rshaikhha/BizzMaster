
using System.Collections;
using System.Collections.Generic;

namespace API.Entities
{
    public class Country : BaseEntity
    {
        public string Abbr { get; set; }
        public string FlagImageUrl { get; set; }

    }

    public class Brand : BaseEntity
    {
        public int CountryId {get; set;}
        public Country Country { get; set; }
        public string LogoImage { get; set; }
        public string TypeHint { get; set; }
    }

    public class Category : BaseEntity 
    { 
        public virtual ICollection<Category> Children {get; set;}
        public string TypeHint {get; set;}
    }

    
}