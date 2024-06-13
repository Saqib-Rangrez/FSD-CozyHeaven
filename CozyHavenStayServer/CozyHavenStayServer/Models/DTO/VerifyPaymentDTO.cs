namespace CozyHavenStayServer.Models.DTO
{
    public class VerifyPaymentDTO
    {
        public string RazorpayOrderId { get; set; }
        public string RazorpayPaymentId { get; set; }
        public string RazorpaySignature { get; set; }
    }
}
