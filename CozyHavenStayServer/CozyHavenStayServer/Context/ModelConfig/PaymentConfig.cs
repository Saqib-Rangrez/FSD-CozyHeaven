using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Set the table name
            builder.ToTable("Payment");

            // Define the primary key
            builder.HasKey(e => e.PaymentId);

            // Define property configurations
            builder.Property(e => e.PaymentId).HasColumnType("int").IsRequired();
            builder.Property(e => e.BookingId).HasColumnType("int").IsRequired();
            builder.Property(e => e.PaymentMode).HasMaxLength(50); 
            builder.Property(e => e.Status).HasMaxLength(50); 
            builder.Property(e => e.Amount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.PaymentDate).HasColumnType("datetime").IsRequired();

            // Define relationships
            builder.HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Payment_Booking");
        }
    }
}
