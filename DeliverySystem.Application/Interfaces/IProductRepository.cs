using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        //===وراثة  IGenericRepository كل حاجة جاية من
    }
}