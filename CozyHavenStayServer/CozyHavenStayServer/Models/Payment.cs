namespace CozyHavenStayServer.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public int? RefundId { get; set; }
        public string? PaymentMode { get; set; } = string.Empty;
        public string? Status { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public virtual Refund? Refund { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}
