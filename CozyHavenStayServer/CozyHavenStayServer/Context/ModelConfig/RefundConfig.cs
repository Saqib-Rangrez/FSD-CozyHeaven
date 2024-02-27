using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class RefundConfig : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            // Set the table name
            builder.ToTable("Refund");

            // Define the primary key
            builder.HasKey(e => e.RefundId);

            // Define property configurations
            builder.Property(e => e.RefundId).HasColumnType("int").IsRequired();
            builder.Property(e => e.PaymentId).HasColumnType("int").IsRequired();
            builder.Property(e => e.RefundAmount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.RefundDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.Reason).HasMaxLength(500);
            builder.Property(e => e.RefundStatus).HasMaxLength(50).HasDefaultValue("Not Approved");

            // Define relationships
            builder.HasOne(e => e.Payment)
                .WithOne(p => p.Refund)
                .HasForeignKey<Refund>(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Refund_Payment");
        }
    }
}
