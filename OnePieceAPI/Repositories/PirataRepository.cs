﻿using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Exceptions.FrutasDelDiablo;
using OnePieceAPI.Exceptions.Piratas;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Services
{
    public class PirataRepository : IPirataRepository
    {
        private readonly OnePieceContext _context;

        public PirataRepository(OnePieceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Pirata>> GetAllPiratasAsync(int page, int pageSize)
        {
            return await _context.Piratas.OrderBy(p => p.Nombre).Skip((page - 1 ) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Pirata?> GetPirataAsync(int pirataId)
        {
            return await _context.Piratas
                .Include(f => f.FrutaDelDiablo)
                .FirstOrDefaultAsync(p => p.Id == pirataId);
        }

        public async Task CreatePirataAsync(Pirata pirata)
        {
            if (pirata == null)
            {
                throw new PirataNoEncontradoException();
            }
            if (pirata.FrutaDelDiabloId.HasValue)
            {
                var fruta = await _context.FrutasDelDiablo.FindAsync(pirata.FrutaDelDiabloId.Value);
                if (fruta == null)
                {
                    throw new FrutaNoEncontradaException(pirata.FrutaDelDiabloId.Value);
                }
            }
            _context.Piratas.Add(pirata);
            await _context.SaveChangesAsync();
        }

        public async Task<Pirata?> UpdatePirataAsync(int id, Pirata pirata)
        {
            if (pirata == null)
            {
                throw new PirataNoEncontradoException();
            }
            var pirataExistente = await _context.Piratas.FindAsync(id);
            if (pirataExistente == null)
            {
                throw new PirataNoEncontradoException(id);
            }
            pirataExistente.Nombre = pirata.Nombre;
            pirataExistente.Descripcion = pirata.Descripcion;
            pirataExistente.Recompensa = pirata.Recompensa;
            pirataExistente.FrutaDelDiabloId = pirata.FrutaDelDiabloId;
            _context.Piratas.Update(pirataExistente);
            await _context.SaveChangesAsync();
            return pirataExistente;
        }

        public async Task<bool> DeletePirataAsync(int id)
        {
            var pirata = await _context.Piratas.FindAsync(id);
            if (pirata == null)
            {
                return false;
            }
            _context.Piratas.Remove(pirata);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}