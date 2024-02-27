using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CozyHavenStayServer.Context.ModelConfig
{
    public class AdminConfig : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("Admin");

            builder.HasKey(a => a.AdminId);

            builder.Property(a => a.AdminId).HasColumnName("AdminId").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(a => a.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
            builder.Property(a => a.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(100);
            builder.Property(a => a.Email).HasColumnName("Email").IsRequired().HasMaxLength(100);
            builder.Property(a => a.Password).HasColumnName("Password").IsRequired().HasMaxLength(255);
            builder.Property(a => a.ProfileImage).HasColumnName("profileImage").IsRequired().HasMaxLength(255);
            builder.Property(a => a.Role).HasColumnName("Role").HasMaxLength(50).HasDefaultValue("Admin");
            builder.Property(x => x.Token).HasColumnName("Token").IsRequired(false);
            builder.Property(x => x.ResetPasswordExpires).HasColumnName("ResetPasswordExpires").IsRequired(false);
        }
    }
}
