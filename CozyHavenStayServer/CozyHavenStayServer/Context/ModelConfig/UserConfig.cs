using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId).HasColumnName("UserId").IsRequired();
            builder.Property(u => u.FirstName).HasColumnName("FirstName").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(u => u.LastName).HasColumnName("LastName").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(u => u.Email).HasColumnName("Email").HasMaxLength(100).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(u => u.Password).HasColumnName("Password").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(u => u.Gender).HasColumnName("Gender").HasMaxLength(50).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(u => u.ContactNumber).HasColumnName("ContactNumber").HasMaxLength(20).HasColumnType("nvarchar(20)").IsRequired();
            builder.Property(u => u.Address).HasColumnName("Address").HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(u => u.Role).HasColumnName("Role").HasMaxLength(50).HasColumnType("nvarchar(50)").HasDefaultValue("Guest");
            builder.Property(u => u.ProfileImage).HasColumnName("ProfileImage").HasMaxLength(255).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Token).HasColumnName("Token").IsRequired(false);
            builder.Property(x => x.ResetPasswordExpires).HasColumnName("ResetPasswordExpires").IsRequired(false);

        }
    }
}
