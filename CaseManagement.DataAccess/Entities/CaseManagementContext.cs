using Microsoft.EntityFrameworkCore;

namespace CaseManagement.DataAccess.Entities;

public partial class CaseManagementContext : DbContext
{
    public CaseManagementContext()
    {
    }

    public CaseManagementContext(DbContextOptions<CaseManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LookupConstant> LookupConstants { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VersionInfo> VersionInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LookupConstant>(entity =>
        {
            entity.ToTable("LookupConstant");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Text).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Otp>(entity =>
        {
            entity.ToTable("OTP");

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.GeneratedAt).HasColumnType("datetime");
            entity.Property(e => e.OtpHash).HasMaxLength(255);
            entity.Property(e => e.RequestedBy).HasMaxLength(26);

            entity.HasOne(d => d.RequestedByNavigation).WithMany(p => p.Otps)
                .HasForeignKey(d => d.RequestedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTP_RequestedBy_Person_Id");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.HasIndex(e => e.Email, "IX_Person_Email").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "IX_User_UserName").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
            entity.Property(e => e.PersonId).HasMaxLength(26);
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_PersonId_Person_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_RoleId_LookupConstant_Id");
        });

        modelBuilder.Entity<VersionInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VersionInfo");

            entity.HasIndex(e => e.Version, "UC_Version")
                .IsUnique()
                .IsClustered();

            entity.Property(e => e.AppliedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1024);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
