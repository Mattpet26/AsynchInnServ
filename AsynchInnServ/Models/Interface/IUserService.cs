using AsynchInnServ.Models.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Interface
{
    public interface IUserService
    {
        public Task<UserDTO> Register(RegisterUser data, ModelStateDictionary modelState);
        public Task<UserDTO> Authenticate(string username, string password);
    }
}
