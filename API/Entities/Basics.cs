
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
        public string Code {get; set;}
        public int Level {get; set;}
        public virtual ICollection<Category> Children {get; set;}

        public string ItemUnit { get; set; }
        public string SetUnit {get; set;}

        public int? UsageTypeId {get; set;}
        public virtual UsageType UsageType { get; set; }

        public int? MasterSystemId { get; set; }
         public virtual MasterSystem MasterSystem { get; set; }
         public string HSCode {get; set;}

        
    }

     public class UsageType : BaseEntity {}
     public class MasterSystem : BaseEntity {}


     public class Contact : BaseEntity 
     {
         public string FirstName { get; set; }
         public string LastName { get; set; }
         public string Initials { get; set; }
         public string Position { get; set; }
         public string Email { get; set; }
         public string Mobile { get; set; }
         public string Mobile2 { get; set; }
         public string Mobile3 { get; set; }
         public string WhatsApp { get; set; }
         public bool IsMale {get; set;}

     }
    
}