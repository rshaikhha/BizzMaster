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
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
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

    public class RoleDto
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }

    }

    public static class AccountDtoManager
    {
        public static List<RoleDto> Roles = new List<RoleDto>{
        new RoleDto{ RoleName = "admin", Name = "ادمین" , Description = "مدیر سیستم" , Group = "عمومی"},
        new RoleDto{ RoleName = "member", Name = "کاربر" , Description = "کاربر عادی" , Group = "عمومی"},
        new RoleDto{ RoleName = "CanInvite", Name = "دعوت از همکاران" , Description = "مجوز دعوت افراد جدید به سیستم" , Group = "کاربران"},
        new RoleDto{ RoleName = "CanManageUsers" , Name = "مدیریت کاربران", Description = "کاربران"},
        new RoleDto{ RoleName = "CanManageSubs" , Name = "مدیریت کاربران زیرمجموعه", Description = "کاربران"},
    };
    }

}