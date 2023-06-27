using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CSharpAssessmentWeek2;
using WebApp.Data;

namespace WebApp.Pages.OrderItems
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderItem != null)
            {
                OrderItem = await _context.OrderItem.ToListAsync();
            }
        }
    }
}
