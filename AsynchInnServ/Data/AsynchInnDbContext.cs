using AsynchInnServ.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsynchInnServ.Data
{
    public class AsynchInnDbContext : DbContext
    {
        public AsynchInnDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
