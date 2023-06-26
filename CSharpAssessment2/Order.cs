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
    public void AddOrderItem(Product product, int quantity)
    {
        var order = new OrderItem()
        {
            Product = product,
            Quantity = quantity
        };
        OrderItems.Add(order);
    }
}
