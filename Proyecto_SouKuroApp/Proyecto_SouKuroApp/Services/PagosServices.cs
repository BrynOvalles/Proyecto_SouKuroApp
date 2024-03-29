﻿using Microsoft.EntityFrameworkCore;
using Proyecto_SouKuroApp.DAL;
using Shared.Models;
using System.Linq.Expressions;

namespace Proyecto_SouKuroApp.Services
{
    public class PagosServices
    {
        private readonly Contexto _contexto;
        public PagosServices(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<bool> Existe(int pagosId)
        {
            return await _contexto.pagos.AnyAsync(c => c.PagoId == pagosId);
        }
        public async Task<bool> Insertar(Pago pago)
        {
            _contexto.pagos.Add(pago);
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Modificar(Pago pago)
        {
            var c = await _contexto.compras.FindAsync(pago.PagoId);
            _contexto.Entry(c!).State = EntityState.Detached;
            _contexto.Entry(pago).State = EntityState.Modified;
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> Guardar(Pago pago)
        {
            if (!await Existe(pago.PagoId))
                return await Insertar(pago);
            else
                return await Modificar(pago);
        }
        public async Task<bool> Eliminar(Pago pago)
        {
            var c = await _contexto.compras.FindAsync(pago.PagoId);
            _contexto.Entry(c!).State = EntityState.Detached;
            _contexto.Entry(pago).State = EntityState.Deleted;
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<Pago?> Buscar(int PagoId)
        {
            return await _contexto.pagos
                .Where(c => c.PagoId == PagoId)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
        public async Task<List<Pago>> Listar(Expression<Func<Pago, bool>> Criterio)
        {
            return await _contexto.pagos
                    .Where(Criterio)
                    .AsNoTracking()
                    .ToListAsync();
        }
    }
}
