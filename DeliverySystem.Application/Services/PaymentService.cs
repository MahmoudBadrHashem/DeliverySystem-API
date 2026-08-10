using DeliverySystem.Application.DTOs.Payments;
using DeliverySystem.Application.Interfaces;
using DeliverySystem.Domain.Entities;
using DeliverySystem.Domain.Enums;

namespace DeliverySystem.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Select(p => new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Method = p.Method.ToString(),
                TransactionId = p.TransactionId,
                Status = p.Status.ToString(),
                PaymentDate = p.PaymentDate
            });
        }

        public async Task<PaymentDto?> GetPaymentByIdAsync(int id)
        {
            var p = await _paymentRepository.GetByIdAsync(id);
            if (p == null) return null;

            return new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Method = p.Method.ToString(),
                TransactionId = p.TransactionId,
                Status = p.Status.ToString(),
                PaymentDate = p.PaymentDate
            };
        }

        public async Task<int> CreatePaymentAsync(CreatePaymentDto dto)
        {
            var payment = new Payment
            {
                OrderId = dto.OrderId,
                Amount = dto.Amount,
                Method = (PaymentMethod)dto.Method,
                Status = PaymentStatus.Pending
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();
            return payment.Id;
        }

        public async Task<bool> UpdatePaymentStatusAsync(int id, UpdatePaymentStatusDto dto)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null) return false;

            payment.Status = (PaymentStatus)dto.Status;
            if (dto.TransactionId != null)
                payment.TransactionId = dto.TransactionId;

            if (payment.Status == PaymentStatus.Paid)
                payment.PaymentDate = DateTime.UtcNow;

            await _paymentRepository.UpdateAsync(payment);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}