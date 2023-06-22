using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectBOX.EntityFrameworkModelFiles;

public partial class ProjectBoxDbContext : DbContext
{
    public ProjectBoxDbContext()
    {
    }

    public ProjectBoxDbContext(DbContextOptions<ProjectBoxDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthorizationDatum> AuthorizationData { get; set; }

    public virtual DbSet<CompleteTask> CompleteTasks { get; set; }

    public virtual DbSet<ContainerDatum> ContainerData { get; set; }

    public virtual DbSet<HistoryOfChangesObjectLocation> HistoryOfChangesObjectLocations { get; set; }

    public virtual DbSet<ObjectDatum> ObjectData { get; set; }

    public virtual DbSet<UserDatum> UserData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ngknn.ru;Database=ProjectBoxDB;user id=23П;password=12357;Trusted_Connection=false;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorizationDatum>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("PK__Authoriz__5E55825A7DDC6169");

            entity.HasIndex(e => e.UserId, "UQ__Authoriz__1788CCADBF282A1F").IsUnique();

            entity.Property(e => e.Login)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.SecurePasssword)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.AuthorizationDatum)
                .HasForeignKey<AuthorizationDatum>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Authoriza__UserI__3A81B327");
        });

        modelBuilder.Entity<CompleteTask>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Complete_tasks");

            entity.Property(e => e.ChangesDate).HasColumnType("datetime");
            entity.Property(e => e.ContainerName)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.HistoryOfChangesObjectLocationDescription).IsUnicode(false);
            entity.Property(e => e.ObjectDataDescription).IsUnicode(false);
            entity.Property(e => e.ObjectId).HasColumnName("ObjectID");
            entity.Property(e => e.ObjectName)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ContainerDatum>(entity =>
        {
            entity.HasKey(e => e.ContainerId).HasName("PK__Containe__0379609B66AD14D8");

            entity.HasIndex(e => e.ContainerName, "UQ__Containe__C59A582F3610EFCB").IsUnique();

            entity.Property(e => e.ContainerId).HasColumnName("ContainerID");
            entity.Property(e => e.ContainerName)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
        });

        modelBuilder.Entity<HistoryOfChangesObjectLocation>(entity =>
        {
            entity.HasKey(e => e.ChangesObjectLocationId).HasName("PK__HistoryO__D6B10CAB14B5C9D0");

            entity.ToTable("HistoryOfChangesObjectLocation");

            entity.Property(e => e.ChangesObjectLocationId).HasColumnName("ChangesObjectLocationID");
            entity.Property(e => e.ChangesDate).HasColumnType("datetime");
            entity.Property(e => e.ContainerId).HasColumnName("ContainerID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.ObjectId).HasColumnName("ObjectID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Container).WithMany(p => p.HistoryOfChangesObjectLocations)
                .HasForeignKey(d => d.ContainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HistoryOf__Conta__440B1D61");

            entity.HasOne(d => d.Object).WithMany(p => p.HistoryOfChangesObjectLocations)
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HistoryOf__Objec__4316F928");

            entity.HasOne(d => d.User).WithMany(p => p.HistoryOfChangesObjectLocations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HistoryOf__UserI__4222D4EF");
        });

        modelBuilder.Entity<ObjectDatum>(entity =>
        {
            entity.HasKey(e => e.ObjectId).HasName("PK__ObjectDa__9A6192B11D5A44C0");

            entity.Property(e => e.ObjectId).HasColumnName("ObjectID");
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.ObjectName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserDatum>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserData__1788CCAC2154537E");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Telephone)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(256)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
