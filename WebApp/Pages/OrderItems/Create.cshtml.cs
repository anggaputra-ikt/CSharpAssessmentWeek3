using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CSharpAssessmentWeek2;
using WebApp.Data;

namespace WebApp.Pages.OrderItems
{
	public class CreateModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public CreateModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult OnGet(string id)
		{
			Order = _context.Orders.FirstOrDefault(o => o.Id == id);
			if (id == null || Order == null)
			{
				return NotFound();
			}
			return Page();
		}

		[BindProperty]
		public OrderItem OrderItem { get; set; } = default!;
		public Order Order { get; set; } = new();
		[BindProperty]
		public Product Product { get; set; }


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.OrderItem == null || _context.Product == null || Order == null || Product == null)
			{
				return Page();
			}

			var product = new Product()
			{
				Name = Product.Name,
				Description = Product.Description,
				Category = Product.Category,
				Price = Product.Price
			};
			
			_context.Product.Add(product);
			_context.OrderItem.Add(OrderItem);

			_context.SaveChanges();

			return RedirectToPage("./Index");
		}
	}
}
