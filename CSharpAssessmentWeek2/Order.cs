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
    public decimal AfterTax { get { return SumTotal(); } }
    public void AddOrderItem(Product product, int quantity)
    {
        var order = new OrderItem()
        {
            Product = product,
            Quantity = quantity
        };
        OrderItems.Add(order);
    }

    private decimal SumTotal()
    {
        decimal total = 0;
        foreach (var item in OrderItems)
        {
            total += item.Quantity * item.Product.Price;
        }
        return total;
    }

    public decimal TaxPpn()
    {
        return SumTotal() * 0.15m;
    }

    public decimal TaxTable()
    {
        return SumTotal() * (DineIn ? 0.15m : 0.5m);
    }

    public decimal ResultTotal()
    {
        return SumTotal() + TaxPpn() + TaxTable();
    }
}
