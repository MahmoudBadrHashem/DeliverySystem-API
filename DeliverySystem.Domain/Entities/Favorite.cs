using System;

namespace DeliverySystem.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; } =default!;
        public int ProductId { get; set; }
        public DateTime CreatedDate { get; set; }

        //= Navigation property //=
        public Product? Product { get; set; }
    }
}