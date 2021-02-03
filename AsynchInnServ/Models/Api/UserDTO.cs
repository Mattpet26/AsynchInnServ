using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models.Api
{
    public class UserDTO
    {
        //what we give back.
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
