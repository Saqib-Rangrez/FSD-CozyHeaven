using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly ILogger<RefundController> _logger;
        private readonly IRefundService _refundServices;

        public RefundController(ILogger<RefundController> logger, IRefundService refundServices)
        {
            _logger = logger;
            _refundServices = refundServices;
        }

        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetAllRefunds")]
        public async Task<ActionResult<List<Refund>>> GetAllRefundsAsync()
        {
            try
            {
                var refunds = await _refundServices.GetAllRefundsAsync();
                if (refunds == null || refunds.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        error = "No data found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = refunds
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving all refunds"
                });
            }
        }

        // Get refund by Id
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetRefundById/{id}")]
        public async Task<ActionResult<Refund>> GetRefundByIdAsync(int id)
        {
            try
            {
                var refund = await _refundServices.GetRefundByIdAsync(id);
                if (refund == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Refund with ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = refund
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving refund by ID"
                });
            }
        }


        // Get refund by PaymentID
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetRefundByPaymentId/{id}")]
        public async Task<ActionResult<Refund>> GetRefundByPaymentIdAsync(int id)
        {
            try
            {
                var refund = await _refundServices.GetRefundByPaymentIdAsync(id);
                if (refund == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Refund with Payment ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = refund
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving refund by Payment ID"
                });
            }
        }

        // Create refund
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpPost]
        [Route("CreateRefund")]
        public async Task<ActionResult<Refund>> CreateRefundAsync([FromBody] Refund refund)
        {
            try
            {
                var createdRefund = await _refundServices.CreateRefundAsync(refund);
                if (createdRefund == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to create refund"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdRefund
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating refund"
                });
            }
        }

        // Update refund
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpPut]
        [Route("UpdateRefund")]
        public async Task<ActionResult<bool>> UpdateRefundAsync([FromBody] Refund refund)
        {
            try
            {
                var success = await _refundServices.UpdateRefundAsync(refund);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to update refund"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Refund updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating refund"
                });
            }
        }

        // Delete refund
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpDelete]
        [Route("DeleteRefund/{id}")]
        public async Task<ActionResult<bool>> DeleteRefundAsync(int id)
        {
            try
            {
                var success = await _refundServices.DeleteRefundAsync(id);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to delete refund"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Refund deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting refund"
                });
            }
        }

        // POST: api/ApproveRefund/{refundId}
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("ApproveRefund/{refundId}")]
        public async Task<ActionResult> ApproveRefundAsync(int refundId)
        {
            try
            {
                var success = await _refundServices.ApproveRefundAsync(refundId);
                if (success)
                {
                    return Ok(new
                    {
                        success = true,
                        message = $"Refund with ID {refundId} has been approved"
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = $"Failed to approve refund with ID {refundId}"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while approving refund with ID {refundId}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = $"An error occurred while approving refund with ID {refundId}"
                });
            }
        }
    }
}
