using System;
using System.Collections.Generic;
using Graduation_App.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Graduation_App.Models;

public partial class MyAppDbContext : DbContext
{
    public MyAppDbContext()
    {
    }

    public MyAppDbContext(DbContextOptions<MyAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Traineecourse> Traineecourses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=graduationAppDb;user=root;password=4455_Ali", ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PRIMARY");

            entity.ToTable("application");

            entity.HasIndex(e => e.TraineeCourseId, "TraineeCourseID");

            entity.HasIndex(e => e.UserId, "UserID");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.Status).HasColumnType("enum('Pending','Accepted','Rejected')");
            entity.Property(e => e.TraineeCourseId).HasColumnName("TraineeCourseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.TraineeCourse).WithMany(p => p.Applications)
                .HasForeignKey(d => d.TraineeCourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("application_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("application_ibfk_1");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PRIMARY");

            entity.ToTable("company");

            entity.HasIndex(e => e.CompanyName, "CompanyName").IsUnique();

            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.AboutCompany).HasColumnType("text");
            entity.Property(e => e.CompanyLogo).HasMaxLength(255);
            entity.Property(e => e.ContactInformation).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Industry).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);
        });

        modelBuilder.Entity<Traineecourse>(entity =>
        {
            entity.HasKey(e => e.TraineeCourseId).HasName("PRIMARY");

            entity.ToTable("traineecourse");

            entity.HasIndex(e => e.CompanyId, "CompanyID");

            entity.Property(e => e.TraineeCourseId).HasColumnName("TraineeCourseID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.ExpectationsFromStudents).HasColumnType("text");
            entity.Property(e => e.Gparequirement)
                .HasPrecision(3, 2)
                .HasColumnName("GPARequirement");
            entity.Property(e => e.TraineeDescription).HasColumnType("text");
            entity.Property(e => e.TraineeTitle).HasMaxLength(255);

            entity.HasOne(d => d.Company).WithMany(p => p.Traineecourses)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("traineecourse_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.EmailAddress, "unique_email").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasColumnType("enum('Male','Female')");
            entity.Property(e => e.GitHubProfile).HasMaxLength(255);
            entity.Property(e => e.Gpa)
                .HasPrecision(3, 2)
                .HasColumnName("GPA");
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.LinkedInProfile).HasMaxLength(255);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Portfolio).HasMaxLength(255);
            entity.Property(e => e.ResumeCv).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'Student'")
                .HasColumnType("enum('Admin','Student')");
            entity.Property(e => e.Skills).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
