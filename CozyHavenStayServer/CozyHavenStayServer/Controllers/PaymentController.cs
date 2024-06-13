using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentServices;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentServices)
        {
            _logger = logger;
            _paymentServices = paymentServices;
        }

        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<ActionResult<List<Payment>>> GetAllPaymentsAsync()
        {
            try
            {
                var payments = await _paymentServices.GetAllPaymentsAsync();
                if (payments == null || payments.Count <= 0)
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
                    data = payments
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving all payments"
                });
            }
        }

        // Get payment by ID
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetPaymentById/{id}")]
        public async Task<ActionResult<Payment>> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _paymentServices.GetPaymentByIdAsync(id);
                if (payment == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Payment with ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = payment
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving payment by ID"
                });
            }
        }

        // Get payment by ID
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetPaymentByBookingId/{id}")]
        public async Task<ActionResult<Payment>> GetPaymentByBookingIdAsync(int id)
        {
            try
            {
                var payment = await _paymentServices.GetPaymentByBookingIdAsync(id);
                if (payment == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Payment with BookingID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = payment
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving payment by BookingID"
                });
            }
        }

        // Create payment
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<ActionResult<Payment>> CreatePaymentAsync([FromBody] Payment payment)
        {
            try
            {   

                var createdPayment = await _paymentServices.CreatePaymentAsync(payment);
                if (createdPayment == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to create payment"
                    });
                }


                return Ok(new
                {
                    success = true,
                    data = createdPayment
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating payment"
                });
            }
        }

        // Update payment
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<ActionResult<bool>> UpdatePaymentAsync([FromBody] Payment payment)
        {
            try
            {
                var success = await _paymentServices.UpdatePaymentAsync(payment);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to update payment"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Payment updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating payment"
                });
            }
        }

        // Delete payment
        [Authorize(Roles = "Admin, Owner")]
        [HttpDelete]
        [Route("DeletePayment/{id}")]
        public async Task<ActionResult<bool>> DeletePaymentAsync(int id)
        {
            try
            {
                var success = await _paymentServices.DeletePaymentAsync(id);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to delete payment"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Payment deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting payment"
                });
            }
        }

    }

}
