using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //===وراثة  IGenericRepository كل حاجة جاية من
    }
}