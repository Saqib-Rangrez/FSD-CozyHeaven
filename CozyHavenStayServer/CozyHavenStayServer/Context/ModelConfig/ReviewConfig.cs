using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(r => r.ReviewId);

            builder.Property(r => r.ReviewId).HasColumnName("ReviewId").IsRequired();
            builder.Property(r => r.UserId).HasColumnName("UserId").IsRequired(); 
            builder.Property(r => r.HotelId).HasColumnName("HotelId").IsRequired();
            builder.Property(r => r.Rating).HasColumnName("Rating").IsRequired();
            builder.Property(r => r.Date).HasColumnName("Date").IsRequired();
            builder.Property(r => r.Comments).HasColumnName("Comments").HasColumnType("nvarchar(max)").IsRequired();

            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Reviews)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Review_Hotel");

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Review_User");
        }
    }
}
