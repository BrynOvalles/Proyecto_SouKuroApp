﻿using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Proyecto_SouKuroApp.DAL
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
        }

        public DbSet<Compra> compras { get; set; }
        public DbSet<Producto> productos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Pago> pagos { get; set; }
        public DbSet<Proveedor> proveedores { get; set; }
        public DbSet<Venta> ventas { get; set; }
        public DbSet<Pedido> pedidos { get; set; }
        public DbSet<Informe> Informes { get; set; }
        public DbSet<Compras_Detalles> detalleCompras { get; set; }
        public DbSet<Producto_Detalle> producto_Detalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasData(new List<Usuario>() {
                new Usuario(){UsuarioId=1, Nombre_Usuario="Admin", Contrasena="123456",Rol="Admin"}
            
            });
        }
    }
}
