using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api.Errors;
using Razorpay.Api;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using CozyHavenStayServer.Models.DTO;
using Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api.Errors;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Collections.Generic;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        //private readonly string apiKey = "rzp_live_eSbRcu4kQn8Sm3";
        //private readonly string apiSecret = "AJcGwBS3OuidpfkPs5effJqW";

        private readonly string apiKey = "rzp_test_SBue5SI9TTwyNF";
        private readonly string apiSecret = "jW8tTQZUvXI5VD5ZiUaeCbib";

        private readonly ILogger<CheckoutController> _logger;

        public CheckoutController(ILogger<CheckoutController> logger)
        {
           
            _logger = logger;
        }

        [HttpPost("create-order")]
        //[Authorize]
        public IActionResult CreateOrder([FromBody] RazorpayDTO model)
        {
            try
            {
                

                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", model.amount * 100); // Razorpay expects amount in paisa/cent
                options.Add("currency", "INR");
                options.Add("payment_capture", 1); // Auto capture payment

                RazorpayClient client = new RazorpayClient(apiKey, apiSecret);
                Razorpay.Api.Order order = client.Order.Create(options);
                string orderId = order["id"].ToString();
                var key = apiKey;
                var totalamount = model.amount;
                var currency = "INR";
                var name = "CozyHeaven Stay";
                var callbackurl = $"https://localhost:7129/api/checkout/verify-payment";

                return Ok(new { order_id = orderId, amount = model.amount, key = key, currency = currency, name = name, callback_url = callbackurl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating order: {Message}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPost("verify-payment")]
        //public async Task<IActionResult> VerifyPayment([FromForm] VerifyPaymentDTO verifyPaymentDTO)
        //{
        //    try
        //    {
        //        Dictionary<string, string> attributes = new Dictionary<string, string>();
        //        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        //        {
        //            string requestBody = await reader.ReadToEndAsync();
        //            var formData = ParseFormData(requestBody);
        //            if (formData.ContainsKey("razorpay_order_id") &&
        //                formData.ContainsKey("razorpay_payment_id") &&
        //                formData.ContainsKey("razorpay_signature"))
        //            {
        //                string razorpayOrderId = formData["razorpay_order_id"];
        //                string razorpayPaymentId = formData["razorpay_payment_id"];
        //                string razorpaySignature = formData["razorpay_signature"];
        //                attributes.Add("razorpay_order_id", razorpayOrderId);
        //                attributes.Add("razorpay_payment_id", razorpayPaymentId);
        //                attributes.Add("razorpay_signature", razorpaySignature);
        //                Utils.verifyPaymentSignature(attributes);
        //                //var res = await _orderRepo.PlaceOrder(userId, "online");
        //                //we have to have an refind in that i need to populate and then mark the payment need to refund
        //                return Ok(new { Status = true });
        //            }
        //            else
        //            {
        //                return BadRequest("Missing required form data.");
        //            }
        //        }
        //    }
        //    catch (SignatureVerificationError ex)
        //    {
        //        _logger.LogError(ex, "Error occurred while verifying payment: {Message}", ex.Message);
        //        return BadRequest("Payment verification failed");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error occurred: {Message}", ex.Message);
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPost("verify-payment")]
        public IActionResult VerifyPayment([FromBody] VerifyPaymentDTO verifyPaymentDto)
        {
            try
            {
                if (verifyPaymentDto == null ||
                    string.IsNullOrEmpty(verifyPaymentDto.RazorpayOrderId) ||
                    string.IsNullOrEmpty(verifyPaymentDto.RazorpayPaymentId) ||
                    string.IsNullOrEmpty(verifyPaymentDto.RazorpaySignature))
                {
                    return BadRequest("Missing required payment data.");
                }
                Dictionary<string, string> attributes = new Dictionary<string, string>
                {
                   { "razorpay_order_id", verifyPaymentDto.RazorpayOrderId },
                   { "razorpay_payment_id", verifyPaymentDto.RazorpayPaymentId },
                   { "razorpay_signature", verifyPaymentDto.RazorpaySignature }
                };
                Utils.verifyPaymentSignature(attributes);
                // var res = await _orderRepo.PlaceOrder(userId, "online");
                // Populate and mark the payment need to refund if necessary
                return Ok(new { Status = true });
            }
            catch (SignatureVerificationError ex)
            {
                _logger.LogError(ex, "Error occurred while verifying payment: {Message}", ex.Message);
                return BadRequest("Payment verification failed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred: {Message}", ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private Dictionary<string, string> ParseFormData(string formData)
        {
            var pairs = formData.Split('&');
            var dict = new Dictionary<string, string>();
            foreach (var pair in pairs)
            {
                var keyValue = pair.Split('=');
                if (keyValue.Length == 2)
                {
                    dict.Add(Uri.UnescapeDataString(keyValue[0]), Uri.UnescapeDataString(keyValue[1]));
                }
                else
                {
                    // Handle cases where key-value pair is not properly formatted
                    _logger.LogWarning("Invalid form data pair: {Pair}", pair);
                }
            }
            return dict;
        }

        private string GetUserFromToken()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "JWT token is null or empty");
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var userId = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "UserId")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "User ID claim is missing or empty");
            }

            return userId;
        }
    
}
}
