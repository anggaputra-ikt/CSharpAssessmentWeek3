using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSharpAssessmentWeek2;

namespace WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = default!;

        public DbSet<CSharpAssessmentWeek2.OrderItem>? OrderItem { get; set; }

        public DbSet<CSharpAssessmentWeek2.Product>? Product { get; set; }
    }
}
