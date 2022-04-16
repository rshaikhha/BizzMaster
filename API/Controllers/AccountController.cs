using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;
        private readonly MessageService _messageSerivce;

        public AccountController(UserManager<User> userManager, TokenService tokenService, MessageService messageSerivce)
        {
            this._messageSerivce = messageSerivce;
            this._userManager = userManager;
            this._tokenService = tokenService;
        }

        /// <summary>
        /// This method
        /// 1- recieves the phone number by which user is trying to login.
        /// 2- generates a code and saves in user 
        /// 3- sends the code via sms to the phone number provided 
        /// 4- if success returns the phone number
        /// </summary>
        [HttpPost("requestCode")]
        public async Task<ActionResult<LoginDto>> requestCode(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);

            if (user == null)
            {
                return Forbid();
            }
            var code = RandomString(5);
            user.LoginCode = code;
            user.LoginCodeValidation = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);

            // However, it always succeeds inspite of not updating the database
            if (!result.Succeeded)
            {
                return NotFound();
            }

            string message = string.Format("کد ورود شما {}", code);
            var smsres = await SendMessage(loginDto.Username, message);
            if (smsres) { return new LoginDto { Username = loginDto.Username }; } else { return NotFound(); }
        }



        /// <summary>
        /// This method
        /// 1- recieves userDto with only username (phone number) and password (password or sms code)
        /// 2- checks if the password matches user password
        /// 3- checks if the code matches user login code and the timespan of code is less than 60 minutes
        /// 4- if success generates a JWT token and returns UserDto with complete data
        /// </summary>
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            //var roles = await _userManager.GetRolesAsync(user);
            if (user == null)
            {
                return Forbid();
            }
            //var min = (DateTime.Now - user.LoginCodeValidation).TotalMinutes;
            var codeAccepted = await AuthorizeCode(loginDto.Password, loginDto.Username);
            var passAccepted = await AuthorizePassword(loginDto.Password, loginDto.Username);
            if (!codeAccepted && !passAccepted)
            {
                ModelState.AddModelError("Password", "Code or Password is not Valid!!!");
                return ValidationProblem();
            }
            return await ToDto(user);
        }


        /// <summary>
        /// This method
        /// 1- recieves userDto 
        /// 2- checks if the password matches user password
        /// 3- checks if the code matches user login code and the timespan of code is less than 60 minutes
        /// 4- if success updates user with complete data
        /// * Password cannot be updated here
        /// </summary>
        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<UserDto>> Update(UserDto updateDto)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.Email = updateDto.Email;
            user.Avatar = updateDto.Avatar;

            var result = await _userManager.UpdateAsync(user);

            // However, it always succeeds inspite of not updating the database
            if (!result.Succeeded)
            {
                return NotFound();
            }

            return await ToDto(user);

        }


        /// <summary>
        /// This method
        /// 1- recieves UpdatePassDto 
        /// 2- checks if the password matches user password
        /// 3- checks if the code matches user login code and the timespan of code is less than 60 minutes
        /// 3- checks if the the two passwords match
        /// 4- if success updates user Password
        /// * Password cannot be updated here
        /// </summary>
        [HttpPost("updatePassword")]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdatePassword(UpdatePassDto updateDto)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var codeAccepted = await AuthorizeCode(updateDto.Password);
            var passAccepted = await AuthorizePassword(updateDto.Password);
            if (!codeAccepted && !passAccepted)
            {
                ModelState.AddModelError("Password", "Code or Old Password is not Valid!!!");
                return ValidationProblem();
            }

            var passWordsMatch = updateDto.NewPass == updateDto.NewPassConfirm;
            if (!passWordsMatch)
            {
                ModelState.AddModelError("NewPassConfirm", "Passwords Do Not Match");
                return ValidationProblem();
            }


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, updateDto.NewPass);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return await ToDto(user);

        }

        [HttpPost("invite")]
        [Authorize(Roles = "CanInvite")]
        public async Task<ActionResult> Invite(LoginDto loginDto)
        {
            var user = new User
            {
                UserName = loginDto.Username,
            };
            var result = await _userManager.CreateAsync(user, "Aoernclsh@328472");

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            await _userManager.AddToRoleAsync(user, "Member");

            var code = RandomString(5);
            user.LoginCode = code;
            user.LoginCodeValidation = DateTime.Now;
            await _userManager.UpdateAsync(user);

            string message = string.Format("کد ورود شما {}", code);
            var smsres = await SendMessage(loginDto.Username, message);
            if (smsres) { return Ok(); } else { return NotFound(); }

        }

        /// this method returns all users' info in UserDto format
        [HttpGet("users")]
        [Authorize]
        public async Task<IList<UserDto>> Users()
        {
            var users = await _userManager.GetUsersInRoleAsync("member");
            var res = new List<UserDto>();
            foreach (var item in users)
            {
                res.Add(await ToDto(item, false));
            }
            return res;
        }

        [HttpPost("AssignRole")]
        [Authorize("admin")]
        public async Task<ActionResult<UserDto>> AssignRole(UserDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            var result = await _userManager.AddToRolesAsync(user, userDto.Roles);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return await ToDto(user);
        }

        

        // [Authorize(Roles = "AccountManager")]
        // [HttpPost("Register")]
        // public async Task<ActionResult> Register(RegisterDto registerDto)
        // {
        //     var user = new User
        //     {
        //         UserName = registerDto.Username,
        //         Email = registerDto.Email
        //     };

        //     var result = await _userManager.CreateAsync(user, registerDto.Password);

        //     if (!result.Succeeded)
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError(error.Code, error.Description);
        //         }

        //         return ValidationProblem();
        //     }

        //     await _userManager.AddToRoleAsync(user, "Member");

        //     return StatusCode(201);
        // }

        [Authorize]
        [HttpGet("currentUser")]
        public async Task<ActionResult<UserDto>> getCurrentUser()
        {
            return await createUserDto();
        }

        private async Task<UserDto> createUserDto()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return await ToDto(user);
        }

        private async Task<UserDto> ToDto(User user, bool token = true)
        {

            return new UserDto
            {
                Username = user.UserName,
                Token = token ? await _tokenService.GenerateToken(user) : "",
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                Avatar = user.Avatar,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }

        private static Random random = new Random();

        private static string RandomString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<bool> AuthorizeCode(string code, string username = "")
        {
            if (username == "" && User.Identity.Name == null) return false;

            var user = username != ""
                ? await _userManager.FindByNameAsync(username)
                : await _userManager.FindByNameAsync(User.Identity.Name);
            return user != null && user.LoginCode == code && (DateTime.Now - user.LoginCodeValidation).TotalMinutes <= 60;
        }

        private async Task<bool> AuthorizePassword(string Password, string username = "")
        {
            if (username == "" && User.Identity.Name == null) return false;

            var user = username != ""
                ? await _userManager.FindByNameAsync(username)
                : await _userManager.FindByNameAsync(User.Identity.Name);

            return await _userManager.CheckPasswordAsync(user, Password);
        }

        private async Task<bool> SendMessage(string To, string message)
        {
            using (var httpClient = new HttpClient())
            {
                var settings = _messageSerivce.GetMessageSettings();
                var url = settings.Url;
                var data = new List<KeyValuePair<string, string>>{
                                  new KeyValuePair<string, string>("username", settings.Username),
                                  new KeyValuePair<string, string>("password", settings.Password),
                                  new KeyValuePair<string, string>("from", settings.From),
                                  new KeyValuePair<string, string>("to", To),
                                  new KeyValuePair<string, string>("text", message),
                              };
                using (var content = new FormUrlEncodedContent(data))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    var SMSResult = await response.Content.ReadAsStringAsync();
                    var RetResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SMSResult>(SMSResult);
                    if (RetResult.RetStatus == 1)
                    {
                        // We just need to return the username (phone number)
                        // username is used to autofill the login form
                        return true;
                    }
                    return false;
                }
            }
        }

    }


    class SMSResult
    {
        public string Value { get; set; }
        public int RetStatus { get; set; }
        public string StrRetStatus { get; set; }
    }
}