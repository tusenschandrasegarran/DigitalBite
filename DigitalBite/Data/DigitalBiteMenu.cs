using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DigitalBite.Models;

namespace DigitalBite.Data
{
    public class DigitalBiteMenu : DbContext
    {
        public DigitalBiteMenu (DbContextOptions<DigitalBiteMenu> options)
            : base(options)
        {
        }

        public DbSet<DigitalBite.Models.Menu> Menu { get; set; }
    }
}
