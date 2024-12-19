using System;
using System.Collections.Generic;
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

    public virtual DbSet<Case> Cases { get; set; }

    public virtual DbSet<CaseFile> CaseFiles { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<LookupConstant> LookupConstants { get; set; }

    public virtual DbSet<Otp> Otps { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<ScheduleHearing> ScheduleHearings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VersionInfo> VersionInfos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Case>(entity =>
        {
            entity.ToTable("Case");

            entity.HasIndex(e => e.CaseNumber, "IX_Case_CaseNumber").IsUnique();

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.AccusedId).HasMaxLength(26);
            entity.Property(e => e.AdvocateId).HasMaxLength(26);
            entity.Property(e => e.CaseNumber).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.VictimId).HasMaxLength(26);

            entity.HasOne(d => d.Accused).WithMany(p => p.CaseAccuseds)
                .HasForeignKey(d => d.AccusedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Case_AccusedId_Person_Id");

            entity.HasOne(d => d.Advocate).WithMany(p => p.CaseAdvocates)
                .HasForeignKey(d => d.AdvocateId)
                .HasConstraintName("FK_Case_AdvocateId_Person_Id");

            entity.HasOne(d => d.CaseStatus).WithMany(p => p.CaseCaseStatuses)
                .HasForeignKey(d => d.CaseStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Case_CaseStatusId_LookupConstant_Id");

            entity.HasOne(d => d.CaseType).WithMany(p => p.CaseCaseTypes)
                .HasForeignKey(d => d.CaseTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Case_CaseTypeId_LookupConstant_Id");

            entity.HasOne(d => d.Victim).WithMany(p => p.CaseVictims)
                .HasForeignKey(d => d.VictimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Case_VictimId_Person_Id");
        });

        modelBuilder.Entity<CaseFile>(entity =>
        {
            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.CaseId).HasMaxLength(26);
            entity.Property(e => e.FileId).HasMaxLength(26);

            entity.HasOne(d => d.Case).WithMany(p => p.CaseFiles)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CaseFiles_CaseId_Case_Id");

            entity.HasOne(d => d.File).WithMany(p => p.CaseFiles)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CaseFiles_FileId_File_Id");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.ToTable("File");

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.FileName).HasMaxLength(128);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.UploadedBy).HasMaxLength(26);

            entity.HasOne(d => d.FileType).WithMany(p => p.Files)
                .HasForeignKey(d => d.FileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_FileTypeId_LookupConstant_Id");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Files)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_File_UploadedBy_Person_Id");
        });

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

            entity.HasOne(d => d.UsedFor).WithMany(p => p.Otps)
                .HasForeignKey(d => d.UsedForId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTP_UsedForId_LookupConstant_Id");
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

        modelBuilder.Entity<Request>(entity =>
        {
            entity.ToTable("Request");

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.CaseId).HasMaxLength(26);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.FileId).HasMaxLength(26);
            entity.Property(e => e.RaisedBy).HasMaxLength(26);

            entity.HasOne(d => d.Case).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_CaseId_Case_Id");

            entity.HasOne(d => d.File).WithMany(p => p.Requests)
                .HasForeignKey(d => d.FileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_FileId_File_Id");

            entity.HasOne(d => d.RaisedByNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RaisedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_RaisedBy_Person_Id");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.RequestRequestStatuses)
                .HasForeignKey(d => d.RequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_RequestStatusId_LookupConstant_Id");

            entity.HasOne(d => d.RequestType).WithMany(p => p.RequestRequestTypes)
                .HasForeignKey(d => d.RequestTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Request_RequestTypeId_LookupConstant_Id");
        });

        modelBuilder.Entity<ScheduleHearing>(entity =>
        {
            entity.ToTable("ScheduleHearing");

            entity.Property(e => e.Id).HasMaxLength(26);
            entity.Property(e => e.CaseId).HasMaxLength(26);
            entity.Property(e => e.JudgeId).HasMaxLength(26);
            entity.Property(e => e.Judgement).HasMaxLength(255);
            entity.Property(e => e.ScheduledAt).HasColumnType("datetime");

            entity.HasOne(d => d.Case).WithMany(p => p.ScheduleHearings)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleHearing_CaseId_Case_Id");

            entity.HasOne(d => d.Judge).WithMany(p => p.ScheduleHearings)
                .HasForeignKey(d => d.JudgeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduleHearing_JudgeId_User_Id");
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
