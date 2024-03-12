using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> _logger;
        private readonly IRepository<Payment> _paymentRepository;

        public PaymentService(ILogger<PaymentService> logger, IRepository<Payment> paymentRepository)
        {
            _logger = logger;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            try
            {
                var createdPayment = await _paymentRepository.CreateAsync(payment);
                if (createdPayment == null)
                {
                    _logger.LogError("Failed to add Payment");
                    return null;
                }
                return createdPayment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetAsync(p => p.PaymentId == id);
                if (payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                    return false;
                }
                await _paymentRepository.DeleteAsync(payment);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Payment>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _paymentRepository.GetAllAsync();
                return payments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Payment>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetAllAsync();
                var paymentList = payment.Where(p => p?.Booking?.UserId == id).ToList();
                if (payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                }
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Payment> GetPaymentByBookingIdAsync(int bookingId)
        {
            try
            {
                var payment = await _paymentRepository.GetAsync(p => p.BookingId == bookingId);
                if (payment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                }
                return payment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdatePaymentAsync(Payment payment)
        {
            try
            {
                var existingPayment = await _paymentRepository.GetAsync(p => p.PaymentId == payment.PaymentId,true);
                if (existingPayment == null)
                {
                    _logger.LogError("Payment not found with given Id");
                    return false;
                }
                await _paymentRepository.UpdateAsync(payment);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
