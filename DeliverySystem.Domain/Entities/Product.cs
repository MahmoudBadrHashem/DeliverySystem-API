using DeliverySystem.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public int StockQuantity { get; set; }

    public bool IsAvailable { get; set; }

    public Branch Branch { get; set; } = null!;

    public Category Category { get; set; } = null!;
    //= Navigation Properties //=
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}