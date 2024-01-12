using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class AdelCastilloTestBackContext : DbContext
{
    public AdelCastilloTestBackContext()
    {
    }

    public AdelCastilloTestBackContext(DbContextOptions<AdelCastilloTestBackContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=ADelCastilloTestBack; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10AB42B58A");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D59466425BB8EE2A");

            entity.ToTable("Cliente");

            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC3945021A7");

            entity.ToTable("Pedido");

            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Pedido__IdClient__2A4B4B5E");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__PedidoDe__E43646A58C62CA57");

            entity.ToTable("PedidoDetalle");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__PedidoDet__IdPed__2D27B809");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__PedidoDet__IdPro__2E1BDC42");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__098892106F190B87");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__IdCate__1B0907CE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
