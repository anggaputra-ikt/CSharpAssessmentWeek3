using CSharpAssessmentWeek2;

public class Order
{
	public Order()
	{
		Id = Guid.NewGuid().ToString();
	}
	public string Id { get; set; }
	public string Customer { get; set; } = "";
	public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
	public bool DineIn { get; set; } = true;
	public bool Status { get; set; } = false;
	public decimal SubTotal { get { return GetSubTotal(); } }
	public decimal Total { get { return 1; } }
	public Promo Promo { get { return GetPromo(); } }
	public void AddOrderItem(Product product, int quantity)
	{
		var order = new OrderItem()
		{
			Product = product,
			Quantity = quantity
		};
		OrderItems.Add(order);
	}

	private decimal GetSubTotal()
	{
		decimal total = 0;
		foreach (var item in OrderItems)
		{
			total += item.Quantity * item.Product.Price;
		}
		return total;
	}

	public decimal GetPpnTax()
	{
		return GetSubTotal() * 0.15m;
	}

	public decimal GetTableTax()
	{
		return GetSubTotal() * (DineIn ? 0.15m : 0.05m);
	}

	public decimal GetAfterTax()
	{
		return GetSubTotal() + GetPpnTax() + GetTableTax();
	}

	public Promo GetPromo()
	{
		var totalQuantity = OrderItems.Count;
		if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
		{
			if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour <= 9)
			{
				Promo.Name = "MarvelousMonday";
				Promo.Discount = GetSubTotal() * 0.1m;
			}
		}
		else if (DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
		{
			if (totalQuantity >= 5)
			{
				Promo.Name = "WednesdaySpecial";
				Promo.Discount = GetSubTotal() * 0.05m;
			}
		}
		else if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
		{
			if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour <= 9)
			{
				foreach (var item in OrderItems)
				{
					if (item.Product.Name == "Cappucino")
					{
						Promo.Name = "FabulousFriday";
						Promo.Discount = GetSubTotal() * 0.2m;
					}
				}
			}
		}
		return Promo;
	}

	public decimal GetTotal()
	{
		return GetAfterTax() - (Promo.Discount);
	}
}
