using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InAndOut.Core.Entities;

namespace InAndOut.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Expense> Expenses { get; set; }


    }
}
