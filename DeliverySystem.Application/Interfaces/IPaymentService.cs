using DeliverySystem.Application.DTOs.Payments;

namespace DeliverySystem.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync(CancellationToken cancellationToken = default);
        Task<PaymentDto?> GetPaymentByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<PaymentDto?> GetPaymentByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<int> CreatePaymentAsync(CreatePaymentDto dto, CancellationToken cancellationToken = default);
        Task<bool> UpdatePaymentStatusAsync(int id, UpdatePaymentStatusDto dto, CancellationToken cancellationToken = default);
    }
}