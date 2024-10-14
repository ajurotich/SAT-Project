using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SAT.Data.EF.Models;

public partial class SatContext : DbContext
{
    public SatContext()
    {
    }

    public SatContext(DbContextOptions<SatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<ScheduledClass> ScheduledClasses { get; set; }

    public virtual DbSet<ScheduledClassStatus> ScheduledClassStatuses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentStatus> StudentStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=SAT;Trusted_Connection=true;MultipleActiveResultSets=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseDescription).IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Curriculum)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasOne(d => d.ScheduledClass).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.ScheduledClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_ScheduledClasses");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollments_Students");
        });

        modelBuilder.Entity<ScheduledClass>(entity =>
        {
            entity.Property(e => e.InstructorName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Scsid).HasColumnName("SCSID");

            entity.HasOne(d => d.Course).WithMany(p => p.ScheduledClasses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduledClasses_Courses");

            entity.HasOne(d => d.Scs).WithMany(p => p.ScheduledClasses)
                .HasForeignKey(d => d.Scsid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduledClasses_ScheduledClassStatuses");
        });

        modelBuilder.Entity<ScheduledClassStatus>(entity =>
        {
            entity.HasKey(e => e.Scsid);

            entity.Property(e => e.Scsid).HasColumnName("SCSID");
            entity.Property(e => e.Scname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SCName");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Major)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ssid).HasColumnName("SSID");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Ss).WithMany(p => p.Students)
                .HasForeignKey(d => d.Ssid)
                .HasConstraintName("FK_Students_StudentStatuses");
        });

        modelBuilder.Entity<StudentStatus>(entity =>
        {
            entity.HasKey(e => e.Ssid);

            entity.Property(e => e.Ssid).HasColumnName("SSID");
            entity.Property(e => e.Ssdescription)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("SSDescription");
            entity.Property(e => e.Ssname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("SSName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
