using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Produccion.Models;

public partial class ProyectoProduccionContext : DbContext
{
    public ProyectoProduccionContext()
    {
    }

    public ProyectoProduccionContext(DbContextOptions<ProyectoProduccionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CantidadKanbane> CantidadKanbanes { get; set; }

    public virtual DbSet<Cpfijo> Cpfijos { get; set; }

    public virtual DbSet<Cpstock> Cpstocks { get; set; }

    public virtual DbSet<Eoq> Eoqs { get; set; }

    public virtual DbSet<JitKanban> JitKanbans { get; set; }

    public virtual DbSet<Ltc> Ltcs { get; set; }

    public virtual DbSet<Luc> Lucs { get; set; }

    public virtual DbSet<MantenimientoCorrectivo> MantenimientoCorrectivos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PROYECTO_PRODUCCION.mssql.somee.com;Database=PROYECTO_PRODUCCION;User Id=Rcairo09_SQLLogin_1;Password=42lbn2kydy;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Discriminator).HasDefaultValue("");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<CantidadKanbane>(entity =>
        {
            entity.HasKey(e => e.IdcantidadKanbanes).HasName("PK_IDCantidadKanbanes");
        });

        modelBuilder.Entity<Cpfijo>(entity =>
        {
            entity.HasKey(e => e.Idcpfijo).HasName("PK_IDCPFijo");
        });

        modelBuilder.Entity<Cpstock>(entity =>
        {
            entity.HasKey(e => e.Idcpstock).HasName("PK_IDCPStock");
        });

        modelBuilder.Entity<Eoq>(entity =>
        {
            entity.HasKey(e => e.Ideoq).HasName("PK_IDEOQ");
        });

        modelBuilder.Entity<JitKanban>(entity =>
        {
            entity.HasKey(e => e.IdJitKanban).HasName("PK_ID_JIT_KANBAN");
        });

        modelBuilder.Entity<Ltc>(entity =>
        {
            entity.HasKey(e => e.Idltc).HasName("PK_IDLTC");
        });

        modelBuilder.Entity<Luc>(entity =>
        {
            entity.HasKey(e => e.Idluc).HasName("PK_IDLUC");
        });

        modelBuilder.Entity<MantenimientoCorrectivo>(entity =>
        {
            entity.HasKey(e => e.Idmantenimiento).HasName("PK_IDMantenimiento");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
