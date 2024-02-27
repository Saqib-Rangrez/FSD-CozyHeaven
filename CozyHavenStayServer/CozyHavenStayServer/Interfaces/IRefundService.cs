using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IRefundService
    {
        Task<Refund> CreateRefundAsync(Refund refund);
        Task<bool> UpdateRefundAsync(Refund refund);
        Task<bool> DeleteRefundAsync(int refundId);
        Task<Refund> GetRefundByIdAsync(int refundId);
        Task<Refund> GetRefundByPaymentIdAsync(int paymentId);

        public Task<List<Refund>> GetAllRefundsAsync();

        Task<bool> ApproveRefundAsync(int refundId);

    }
}
