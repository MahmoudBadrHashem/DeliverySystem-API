using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Infrastructure.Persistence;

namespace DeliverySystem.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        //= دي بنعملها عشان الميثود العادية بتجيب المنتج لوحده من غير تفاصيله  Override ال
        public override async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Branch)
                .ToListAsync();
        //= هنا لما نيجي نطلب منتج معين بنخليه يجيب معاهم التصنيف والفرع بتاعه عشان الداتا تطلع كاملة للعميل ومش ناقصة اي حاجه 
        public override async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Branch)
                .FirstOrDefaultAsync(p => p.Id == id);
    }
}