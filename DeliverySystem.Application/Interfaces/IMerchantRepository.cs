using DeliverySystem.Domain.Entities;

namespace DeliverySystem.Application.Interfaces
{
    public interface IMerchantRepository : IGenericRepository<Merchant>
    {
        //===وراثة  IGenericRepository كل حاجة جاية من
    }
}