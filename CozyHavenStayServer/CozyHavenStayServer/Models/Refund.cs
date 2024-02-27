namespace CozyHavenStayServer.Models
{
    public class Refund
    {
        public int RefundId { get; set; }
        public int PaymentId { get; set; }
        public decimal RefundAmount { get; set; }
        public DateTime RefundDate { get; set; }

        public string? Reason { get; set; }
        public string? RefundStatus { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
