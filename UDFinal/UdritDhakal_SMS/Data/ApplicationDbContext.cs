using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UdritDhakal_SMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
               .Property(p => p.FirstName)
               .IsRequired()
               .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
               .Property(p => p.LastName)
               .IsRequired()
               .HasMaxLength(100);



            builder.Entity<ApplicationUser>()
               .Property(p => p.Address)
               .IsRequired()
               .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
               .Property(p => p.PhoneNumber)
               .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
               .Property(p => p.IsActive)
               .IsRequired()
               .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
               .Property(p => p.HasEnrolled)
               .IsRequired()
               .HasMaxLength(100);

            builder.Entity<ApplicationUser>()
               .Property(p => p.CreatedBy)
               .IsRequired();

            builder.Entity<ApplicationUser>()
               .Property(p => p.CreatedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.Entity<ApplicationUser>()
               .Property(p => p.ModifiedDate)
               .IsRequired()
               .HasDefaultValueSql("GETDATE()")
               .HasColumnType("DATETIME");

            builder.Entity<IdentityRole>()
               .ToTable("Roles")
               .Property(p => p.Id)
               .HasColumnName("RoleId");
        }


    }
}
