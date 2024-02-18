using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class HotelConfig : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {

            builder.HasKey(h => h.HotelId);

            builder.Property(h => h.HotelId).HasColumnName("HotelId").IsRequired();
            builder.Property(h => h.OwnerId).HasColumnName("OwnerId").IsRequired();
            builder.Property(h => h.Name).HasColumnName("Name").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(h => h.Location).HasColumnName("Location").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(h => h.Description).HasColumnName("Description").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(h => h.Amenities).HasColumnName("Amenities").HasColumnType("nvarchar(max)").IsRequired();

            builder.HasOne(h => h.Owner)
                .WithMany(o => o.Hotels)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Hotel_HotelOwner");

        }
    }
}
