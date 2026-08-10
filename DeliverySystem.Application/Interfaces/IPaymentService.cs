using DeliverySystem.Application.DTOs.Payments;

namespace DeliverySystem.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
        Task<PaymentDto?> GetPaymentByIdAsync(int id);
        Task<int> CreatePaymentAsync(CreatePaymentDto dto);
        Task<bool> UpdatePaymentStatusAsync(int id, UpdatePaymentStatusDto dto);
    }
}