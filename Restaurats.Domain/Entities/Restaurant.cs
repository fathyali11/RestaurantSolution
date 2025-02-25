namespace Restaurats.Domain.Entities;
public class Restaurant
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool HasDelivery { get; set; }

    public string ContactEmail { get; set; }=string.Empty;
    public string ContactNumber { get; set; } = string.Empty;

    public Address Address { get; set; } = default!;
    public List<Dish> Dishes { get; set; } = [];
}
