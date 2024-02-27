using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class HotelOwnerConfig : IEntityTypeConfiguration<HotelOwner>
    {
        public void Configure(EntityTypeBuilder<HotelOwner> builder)
        {
            builder.ToTable("HotelOwners");

            builder.HasKey(ho => ho.OwnerId);


            builder.Property(ho => ho.FirstName).HasColumnName("FirstName").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(ho => ho.LastName).HasColumnName("LastName").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(ho => ho.Email).HasColumnName("Email").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(ho => ho.Password).HasColumnName("Password").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(ho => ho.Gender).HasColumnName("Gender").HasMaxLength(50).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(ho => ho.ContactNumber).HasColumnName("ContactNumber").HasMaxLength(20).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(ho => ho.Address).HasColumnName("Address").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(ho => ho.ProfileImage).HasColumnName("ProfileImage").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(ho => ho.Role).HasColumnName("Role").HasMaxLength(50).HasColumnType("nvarchar(50)").HasDefaultValue("Owner");
            builder.Property(x => x.Token).HasColumnName("Token").IsRequired(false);
            builder.Property(x => x.ResetPasswordExpires).HasColumnName("ResetPasswordExpires").IsRequired(false);

        }
    }
}
