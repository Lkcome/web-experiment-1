using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace shiyan1.DatabaseModels;

public partial class _2109060123DbContext : DbContext
{
    public _2109060123DbContext()
    {
    }

    public _2109060123DbContext(DbContextOptions<_2109060123DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<SelectedCourse> SelectedCourses { get; set; }
    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Admins> Admins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("persist security info=True;data source=rm-bp1tg219t7o5j5ex8fo.rwlb.rds.aliyuncs.com;port=3306;initial catalog=2109060123_db;user id=2109060123;password=2109060123;character set=utf8;allow zero datetime=true;convert zero datetime=true;pooling=true;maximumpoolsize=3000", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PRIMARY");
            entity.ToTable("course");
            entity.Property(e => e.Cid).HasMaxLength(15);
            entity.Property(e => e.Cname).HasMaxLength(50);
            entity.Property(e => e.Cscore).HasMaxLength(15);
            entity.Property(e => e.Cteacher).HasMaxLength(50);
            entity.Property(e => e.Csem).HasMaxLength(20);
            entity.Property(e => e.Ctime).HasMaxLength(50);
            entity.Property(e => e.Cclassroom).HasMaxLength(50);
        });

        modelBuilder.Entity<SelectedCourse>(entity =>
        {
            entity.HasKey(e => new { e.Cid, e.Sid })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
            entity.ToTable("selected_course");
            entity.Property(e => e.Cid).HasMaxLength(15);
            entity.Property(e => e.Sid).HasMaxLength(12);
            entity.HasOne(e => e.Course)
                .WithMany(c => c.SelectedCourses)
                .HasForeignKey(e => e.Cid);
            entity.HasOne(e => e.Student)
                .WithMany(s => s.SelectedCourses)
                .HasForeignKey(e => e.Sid);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PRIMARY");
            entity.ToTable("student");
            entity.Property(e => e.Sid).HasMaxLength(12);
            entity.Property(e => e.Sclass).HasMaxLength(50);
            entity.Property(e => e.Sname).HasMaxLength(50);
            entity.Property(e => e.Spassword).HasMaxLength(20);
        });

        modelBuilder.Entity<Admins>(entity =>
        {
            entity.HasKey(e => e.Aid).HasName("PRIMARY");
            entity.ToTable("admins");
            entity.Property(e => e.Aaccount).HasMaxLength(50);
            entity.Property(e => e.Apassword).HasMaxLength(50);;
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
