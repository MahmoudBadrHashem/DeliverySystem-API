namespace DeliverySystem.Domain.Entities
{
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        //  التاجر الواحد بيكون عنده أكتر من فرع
        public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    }
}