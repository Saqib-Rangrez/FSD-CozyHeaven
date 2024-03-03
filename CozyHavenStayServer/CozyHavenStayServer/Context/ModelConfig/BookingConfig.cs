using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class BookingConfig : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {

            builder.ToTable("Booking");

            builder.HasKey(e => e.BookingId);

            builder.Property(e => e.BookingId).HasColumnType("int").IsRequired();
            builder.Property(e => e.NumberOfGuests).HasColumnType("int").IsRequired();
            builder.Property(e => e.CheckInDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.CheckOutDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.TotalFare).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.Status).HasMaxLength(30).IsRequired();
            builder.Property(e => e.RoomId).HasColumnType("int").IsRequired();
            builder.Property(e => e.UserId).HasColumnType("int").IsRequired();
            builder.Property(e => e.PaymentId).HasColumnType("int").IsRequired(false);

            builder.HasOne(e => e.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(e => e.RoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Booking_Room");

            builder.HasOne(e => e.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Booking_User");

            builder.HasOne(e => e.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Booking_Hotel");
        }
    }
}
