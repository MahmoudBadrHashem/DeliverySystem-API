using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class MerchantRepository : GenericRepository<Merchant>, IMerchantRepository
    {
        public MerchantRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}