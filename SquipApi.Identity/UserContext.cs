using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SquipApi.Pocos;

namespace SquipApi.Identity
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
