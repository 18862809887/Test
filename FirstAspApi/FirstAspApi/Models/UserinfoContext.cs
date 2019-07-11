using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstAspApi.Models
{
    public class UserinfoContext : DbContext
    {
        public UserinfoContext (DbContextOptions<UserinfoContext> options)
            : base(options)
        {
        }

        public DbSet<FirstAspApi.Models.Userinfo> Userinfo { get; set; }
    }
}
