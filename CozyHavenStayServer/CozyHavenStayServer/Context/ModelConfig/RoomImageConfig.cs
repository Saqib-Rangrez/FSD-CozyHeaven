using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class RoomImageConfig : IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.ToTable("RoomImages");

            builder.HasKey(ri => ri.ImageId);

            builder.Property(ri => ri.ImageId).HasColumnName("ImageId").IsRequired();
            builder.Property(ri => ri.RoomId).HasColumnName("RoomId").IsRequired();
            builder.Property(ri => ri.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();

            builder.HasOne(ri => ri.Room)
                .WithMany(r => r.RoomImages)
                .HasForeignKey(ri => ri.RoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RoomImage_Room");
        }
    }
}
