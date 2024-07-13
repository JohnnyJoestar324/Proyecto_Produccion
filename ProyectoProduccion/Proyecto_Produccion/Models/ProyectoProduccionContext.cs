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

    public virtual DbSet<Cpfijo> Cpfijos { get; set; }

    public virtual DbSet<Cpstock> Cpstocks { get; set; }

    public virtual DbSet<Eoq> Eoqs { get; set; }

    public virtual DbSet<Ltc> Ltcs { get; set; }

    public virtual DbSet<Luc> Lucs { get; set; }

    public virtual DbSet<MantenimientoCorrectivo> MantenimientoCorrectivos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
