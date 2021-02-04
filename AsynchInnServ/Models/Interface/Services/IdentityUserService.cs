using AsynchInnServ.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface.Services
{
    public class IdentityUserService : IUserService
    {
        //inject a usermanager
        private UserManager<AppUser> UserManager;
        private JwtTokenService tokenService;
        public IdentityUserService(UserManager<AppUser> manager, JwtTokenService jwtTokenService)
        {
            UserManager = manager;
            tokenService = jwtTokenService;
        }

        public async Task<UserDTO> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };

            //identity does the hashing for passwords for us! Neat!
            //identity also comes with requirements. Password must be a length, contain numbers, uppercase, ect.

            var result = await UserManager.CreateAsync(user, data.Password);

            //if the result succeeds, we create a dto user and return it
            if (result.Succeeded)
            {
                //The data.Roles is returning a cancellationtoken?
                await UserManager.AddToRolesAsync(user, data.Roles);
                UserDTO newuser = new UserDTO
                {
                    Username = user.UserName,
                    Id = user.Id,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
                };
                return newuser;
            }

            //put errors into modelstate. Model state is a dictionary, so use keys
            //Ternary: if ? goodthing : badthing
            // var foo = conditionTrue ? goodthinghappen iffalse badthinghappens
            //triply nested turnary
            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                     "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (await UserManager.CheckPasswordAsync(user, password))
            {
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))
                };
            }
            return null;
        }
        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await UserManager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5))

            };
        }
    }
}
