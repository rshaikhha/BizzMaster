using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public string Avatar {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
    }


    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

        public class UpdatePassDto : LoginDto
    {
        public string NewPass { get; set; }
        public string NewPassConfirm { get; set; }
    }
}