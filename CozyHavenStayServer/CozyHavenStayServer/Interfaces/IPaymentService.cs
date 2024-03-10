using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IPaymentService
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<bool> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId);
        Task<List<Payment>> GetPaymentByIdAsync(int paymentId);
        Task<Payment> GetPaymentByBookingIdAsync(int bookingId);

        public Task<List<Payment>> GetAllPaymentsAsync();

    }
}
