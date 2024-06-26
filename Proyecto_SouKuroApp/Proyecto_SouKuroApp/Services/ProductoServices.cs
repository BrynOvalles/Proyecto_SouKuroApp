﻿using Microsoft.EntityFrameworkCore;
using Proyecto_SouKuroApp.Data;
using Shared.Models;
using System.Linq.Expressions;

namespace Proyecto_SouKuroApp.Services;

public class ProductoServices
{
    private readonly ApplicationDbContext _contexto;
    public ProductoServices(ApplicationDbContext contexto)
    {
        _contexto = contexto;
    }
    public async Task<bool> Existe(int productoId)
    {
        return await _contexto.productos.AnyAsync(c => c.ProductoId == productoId);
    }
    public async Task<Producto?> GetProducto(int id)
    {
        return await _contexto.productos.Include(c => c.Detalle).FirstOrDefaultAsync(p => p.ProductoId == id);
    }
    public async Task<bool> Insertar(Producto producto)
    {
        _contexto.productos.Add(producto);
        int filasAfectadas = await _contexto.SaveChangesAsync();
        return filasAfectadas > 0;
    }
    public async Task<bool> Modificar(Producto producto)
    {
        var c = await _contexto.productos.FindAsync(producto.ProductoId);
        _contexto.Entry(c!).State = EntityState.Detached;
        _contexto.Entry(producto).State = EntityState.Modified;
        return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Producto producto)
    {
        if (!await Existe(producto.ProductoId))
            return await Insertar(producto);
        else
            return await Modificar(producto);
    }
    public async Task<bool> Eliminar(Producto producto)
    {
        var c = await _contexto.productos.FindAsync(producto.ProductoId);
        _contexto.Entry(c!).State = EntityState.Detached;
        _contexto.Entry(producto).State = EntityState.Deleted;
        return await _contexto.SaveChangesAsync() > 0;
    }
    public async Task<Producto?> Buscar(int productoId)
    {
        return await _contexto.productos
            .Where(c => c.ProductoId == productoId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
    }
    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> Criterio)
    {
        return await _contexto.productos
                .Where(Criterio)
                .AsNoTracking()
                .ToListAsync();
    }
}
