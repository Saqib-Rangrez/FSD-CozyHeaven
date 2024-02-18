using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class HotelImageConfig : IEntityTypeConfiguration<HotelImage>
    {
        public void Configure(EntityTypeBuilder<HotelImage> builder)
        {
            builder.ToTable("HotelImages");

            builder.HasKey(hi => hi.ImageId);

            builder.Property(hi => hi.ImageId).HasColumnName("ImageId").IsRequired();
            builder.Property(hi => hi.HotelId).HasColumnName("HotelId").IsRequired();
            builder.Property(hi => hi.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();

            builder.HasOne(hi => hi.Hotel)
                .WithMany(h => h.HotelImages)
                .HasForeignKey(hi => hi.HotelId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_HotelImage_Hotel");
        }
    }
}
