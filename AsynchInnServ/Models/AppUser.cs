using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Models
{
    public class AppUser : IdentityUser
    {
        //we inherit from IdentityUser, which has everything we need
        //Therefor, no code! Whoop whoop!
    }
}
