using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FPTBook_System.Models;
using FPTBook_System.Controllers;

namespace FPTBook_System.Data
{
    public class FPTBook_SystemContext : DbContext
    {
        public FPTBook_SystemContext (DbContextOptions<FPTBook_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<FPTBook_System.Models.book> book { get; set; } = default!;

        public DbSet<FPTBook_System.Controllers.useraccounts>? useraccounts { get; set; }
    }
}
