namespace CSharpAssessmentWeek2
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal Price { get; set; }
    }
}