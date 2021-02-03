using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Api
{
    public class RegisterUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }


        //optional
        public string PhoneNumber { get; set; }
    }
}
