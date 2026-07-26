

namespace DeliverySystem.Domain.Entities
{
    public class Branch
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool IsActive { get; set; }

      //== Navigation Properties//==
        public Merchant Merchant { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}






