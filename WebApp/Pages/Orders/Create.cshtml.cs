using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpAssessmentWeek2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;

namespace WebApp.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.ApplicationDbContext _context;

        public CreateModel(WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = new();
        public List<OrderItem> OrderItems { get; set; } = new();
        [BindProperty]
        public string Product { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        [BindProperty]
        public string Submit { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Orders == null || Order == null || Submit == "+ Add Product")
            {
                var product = new Product()
                {
                    Name = Product,
                    Description = Product,
                    Category = Product,
                    Price = 0
                };
                var orderItem = new OrderItem()
                {
                    Product = product,
                    Quantity = Quantity,
                };
                OrderItems.Add(orderItem);
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
