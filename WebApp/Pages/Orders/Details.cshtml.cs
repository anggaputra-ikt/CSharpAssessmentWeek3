using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CSharpAssessmentWeek2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Pages.Orders
{
	public class DetailsModel : PageModel
	{
		private readonly WebApp.Data.ApplicationDbContext _context;

		public DetailsModel(WebApp.Data.ApplicationDbContext context)
		{
			_context = context;
		}

        [BindProperty]
        public Order Order { get; set; } = default!;
		public List<SelectListItem> ProductOptions { get; set; }
		[BindProperty, DisplayName("Product")]
		public string ProductId { get; set; }
		[BindProperty, DisplayName("Product Quantity")]
		public int ProductQuantity { get; set; }
		public async Task<IActionResult> OnGetAsync(string id)
		{
			if (id == null || _context.Orders == null)
			{
				return NotFound();
			}

			var order = await _context.Orders
				.Include(o => o.OrderItems)
				.ThenInclude(i => i.Product)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (order == null)
			{
				return NotFound();
			}
			else
			{
				Order = order;
			}
            ProductOptions = _context.Product.Select(p => new SelectListItem
			{
				Value = p.Id,
				Text = p.Name,
			}).ToList();
			return Page();
		}

		public IActionResult OnPost()
		{
            if (!ModelState.IsValid || _context.Orders == null || _context.Product == null || Order == null)
            {
                return Page();
            }

            var product = _context.Product.FirstOrDefault(p => p.Id == ProductId);

            var order = _context.Orders.FirstOrDefault(o => o.Id == Order.Id);
            if (order != null)
            {
                order.OrderItems.Add(new OrderItem() { Product = product, Quantity = ProductQuantity });

                _context.Orders.Update(order);
                _context.SaveChanges();
            }

            return Redirect($"/Orders/Details?id={Order.Id}");
        }

		public IActionResult OnPostCheckout()
		{
            var order = _context.Orders.FirstOrDefault(o => o.Id == Order.Id);
            if (order != null)
            {
                order.Status = true;
                _context.Orders.Update(order);
                _context.SaveChanges();
            }
            return Redirect($"/Orders/Details?id={Order.Id}");
        }
	}
}
