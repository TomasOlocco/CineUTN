using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos.Models;

namespace Web.Repos;

public partial class CineUTNContext : DbContext
{
    public CineUTNContext()
    {
    }

    public CineUTNContext(DbContextOptions<CineUTNContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Sonido> Sonidos { get; set; }

    public virtual DbSet<Subtitulo> Subtitulo { get; set; }

    public virtual DbSet<Tipo> Tipo { get; set; }

    public virtual DbSet<Pelicula> Pelicula { get; set; }

    public virtual DbSet<Tarifa> Tarifa { get; set; }

    public virtual DbSet<Sala> Sala { get; set; }

    public virtual DbSet<Programar> Programar { get; set; }

    public virtual DbSet<PedidoItem> PedidoItem { get; set; }

    public virtual DbSet<Pedido> Pedido { get; set; }

    public virtual DbSet<ListaPrecio> ListaPrecio { get; set; }

    public virtual DbSet<Funcion> Funcion { get; set; }

    public virtual DbSet<FuncionTarifa> FuncionTarifa { get; set; }

    public virtual DbSet<CondicionPago> CondicionPago { get; set; }





    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("name=conexion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
