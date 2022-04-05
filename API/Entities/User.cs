using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser
    {

        public string LoginCode { get; set; }
        public DateTime LoginCodeValidation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string inviter { get; set; }
        public string Avatar {get; set;}
        
    }
}