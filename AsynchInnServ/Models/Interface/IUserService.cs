using AsynchInnServ.Models.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IUserService
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="data"></param>
        /// <param name="modelState"></param>
        /// <returns>userDTO that registered</returns>
        public Task<UserDTO> Register(RegisterUser data, ModelStateDictionary modelState);

        /// <summary>
        /// authenticates a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>authed userDTO</returns>
        public Task<UserDTO> Authenticate(string username, string password);

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>userDTO</returns>
        public Task<UserDTO> GetUser(ClaimsPrincipal user);
    }
}
