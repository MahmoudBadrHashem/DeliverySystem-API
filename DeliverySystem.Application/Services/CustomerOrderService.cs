using DeliverySystem.Application.DTOs.CustomerOrders;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Domain.Enums;

namespace DeliverySystem.Application.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerOrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            ICouponRepository couponRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _couponRepository = couponRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateOrderAsync(string userId, CreateCustomerOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new InvalidOperationException("لازم يكون فيه منتج واحد على الأقل في الأوردر");

            // 1. هات المنتجات الحقيقية من الداتابيز (مش من العميل)
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new InvalidOperationException($"المنتج رقم {itemDto.ProductId} غير موجود");

                if (!product.IsAvailable)
                    throw new InvalidOperationException($"المنتج '{product.Name}' غير متاح حالياً");

                if (product.StockQuantity < itemDto.Quantity)
                    throw new InvalidOperationException($"الكمية المطلوبة من '{product.Name}' غير متوفرة");

                // السعر بييجي من الداتابيز، مش من العميل
                var subtotal = product.Price * itemDto.Quantity;
                totalAmount += subtotal;

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    UnitPrice = product.Price,
                    Subtotal = subtotal
                });

                // نقص الكمية من المخزون
                product.StockQuantity -= itemDto.Quantity;
            }

            // 2. طبّق الكوبون لو موجود
            decimal discountAmount = 0;
            if (dto.CouponId.HasValue)
            {
                var coupon = await _couponRepository.GetByIdAsync(dto.CouponId.Value);
                if (coupon == null || !coupon.IsActive)
                    throw new InvalidOperationException("الكوبون غير صالح");

                if (coupon.ExpiryDate < DateTime.UtcNow)
                    throw new InvalidOperationException("الكوبون منتهي الصلاحية");

                if (coupon.UsageLimit.HasValue && coupon.TimesUsed >= coupon.UsageLimit.Value)
                    throw new InvalidOperationException("الكوبون وصل للحد الأقصى للاستخدام");

                discountAmount = coupon.IsPercentage
                    ? totalAmount * (coupon.DiscountAmount / 100)
                    : coupon.DiscountAmount;

                if (discountAmount > totalAmount)
                    discountAmount = totalAmount;

                coupon.TimesUsed++;
            }

            // 3. اعمل الأوردر
            var order = new Order
            {
                UserId = userId,
                BranchId = dto.BranchId,
                AddressId = dto.AddressId,
                CouponId = dto.CouponId,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount - discountAmount,
                DiscountAmount = discountAmount,
                CreatedDate = DateTime.UtcNow,
                OrderItems = orderItems
            };

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();   // كل حاجة بتتحفظ مع بعض في عملية واحدة

            return order.Id;
        }

        public async Task<IEnumerable<CustomerOrderDto>> GetMyOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByCustomerAsync(userId);
            return orders.Select(o => new CustomerOrderDto
            {
                Id = o.Id,
                BranchId = o.BranchId,
                Status = o.Status.ToString(),
                TotalAmount = o.TotalAmount,
                DiscountAmount = o.DiscountAmount,
                CreatedDate = o.CreatedDate,
                DeliveredDate = o.DeliveredDate
            });
        }

        public async Task<CustomerOrderDto?> GetMyOrderByIdAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

         
            if (order == null || order.UserId != userId)
                return null;

            return new CustomerOrderDto
            {
                Id = order.Id,
                BranchId = order.BranchId,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                DiscountAmount = order.DiscountAmount,
                CreatedDate = order.CreatedDate,
                DeliveredDate = order.DeliveredDate
            };
        }

        public async Task<bool> CancelOrderAsync(string userId, int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.UserId != userId)
                return false;

           
            if (order.Status != OrderStatus.Pending)
                throw new InvalidOperationException("لا يمكن إلغاء الأوردر بعد قبوله من المطعم");

            order.Status = OrderStatus.Cancelled;
            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}