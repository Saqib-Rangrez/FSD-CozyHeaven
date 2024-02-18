using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room");

            builder.HasKey(r => r.RoomId);

            builder.Property(r => r.RoomId).HasColumnName("RoomId").IsRequired();
            builder.Property(r => r.HotelId).HasColumnName("HotelId").IsRequired();
            builder.Property(r => r.RoomType).HasColumnName("RoomType").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(r => r.MaxOccupancy).HasColumnName("MaxOccupancy").IsRequired();
            builder.Property(r => r.BedType).HasColumnName("BedType").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(r => r.BaseFare).HasColumnName("BaseFare").HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(r => r.RoomSize).HasColumnName("RoomSize").HasMaxLength(50).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(r => r.Acstatus).HasColumnName("Acstatus").HasMaxLength(50).HasColumnType("nvarchar(50)").IsRequired();

            builder.HasOne(r => r.Hotel)
                .WithMany(h => h.Rooms)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Room_Hotel");

        }
    }
}
