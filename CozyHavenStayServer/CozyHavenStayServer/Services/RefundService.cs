using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Services
{
    public class RefundService : IRefundService
    {
        private readonly ILogger<RefundService> _logger;
        private readonly IRepository<Refund> _refundRepository;

        public RefundService(ILogger<RefundService> logger, IRepository<Refund> refundRepository)
        {
            _logger = logger;
            _refundRepository = refundRepository;
        }

        public async Task<Refund> CreateRefundAsync(Refund refund)
        {
            try
            {
                var createdRefund = await _refundRepository.CreateAsync(refund);
                if (createdRefund == null)
                {
                    _logger.LogError("Failed to add Refund");
                    return null;
                }
                return createdRefund;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteRefundAsync(int id)
        {
            try
            {
                var refund = await _refundRepository.GetAsync(r => r.RefundId == id);
                if (refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                    return false;
                }
                await _refundRepository.DeleteAsync(refund);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Refund>> GetAllRefundsAsync()
        {
            try
            {
                var refunds = await _refundRepository.GetAllAsync();
                return refunds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Refund> GetRefundByIdAsync(int id)
        {
            try
            {
                var refund = await _refundRepository.GetAsync(r => r.RefundId == id);
                if (refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                }
                return refund;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Refund> GetRefundByPaymentIdAsync(int paymentId)
        {
            try
            {
                var refund = await _refundRepository.GetAsync(r => r.PaymentId == paymentId);
                if (refund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                }
                return refund;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateRefundAsync(Refund refund)
        {
            try
            {
                var existingRefund = await _refundRepository.GetAsync(r => r.RefundId == refund.RefundId);
                if (existingRefund == null)
                {
                    _logger.LogError("Refund not found with given Id");
                    return false;
                }
                await _refundRepository.UpdateAsync(refund);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> ApproveRefundAsync(int refundId)
        {
            try
            {
                var refund = await _refundRepository.GetAsync(r => r.RefundId == refundId, true);
                if (refund == null)
                {
                    _logger.LogError($"Refund with ID {refundId} not found");
                    return false;
                }

                // Update refund status to "Approved"
                refund.RefundStatus = "Approved";
                await _refundRepository.UpdateAsync(refund);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while approving refund with ID {refundId}: {ex.Message}");
                return false;
            }
        }
    }
}
