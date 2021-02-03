using AsynchInnServ.Models;
using AsynchInnServ.Models.Api;
using AsynchInnServ.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService Userservice;
        public UserController(IUserService _context)
        {
            Userservice = _context;
        }

        [HttpPost("Register")]
        //this.ModelState ------- comes directly in from MVC / Models

        //Either we get a good user OR we throw a modelstate error.
        public async Task<ActionResult<UserDTO>> Register(RegisterUser data)
        {
            var user = await Userservice.Register(data, this.ModelState);
            if (ModelState.IsValid)
            {
                return user;
            }
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginData data)
        {
            //authentication verifies who you are
            //authorization says what you can do
            var user = await Userservice.Authenticate(data.Username, data.Password);

            if (user != null)
            {
                return user;
            }
            return Unauthorized();
        }
    }
}
