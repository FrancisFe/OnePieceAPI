using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Exceptions.FrutasDelDiablo;
using OnePieceAPI.Exceptions.Piratas;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Repositories.Interfaces;

namespace OnePieceAPI.Services
{
    public class PirataRepository : IPirataRepository
    {
        private readonly OnePieceContext _context;

        public PirataRepository(OnePieceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Pirata> GetQueryable()
        {
            return _context.Piratas
                .Include(p => p.FrutaDelDiablo)
                .Include(p => p.Tripulacion)
                .AsNoTracking();
        }

        public async Task<Pirata?> GetAsync(int pirataId)
        {
            return await _context.Piratas
                .Include(f => f.FrutaDelDiablo)
                .Include(t => t.Tripulacion)
                .FirstOrDefaultAsync(p => p.Id == pirataId);
        }

        public async Task CreateAsync(Pirata pirata)
        {
            if (pirata == null)
            {
                throw new ArgumentNullException(nameof(pirata), "El pirata no puede ser nulo.");

            }
            if (pirata.FrutaDelDiabloId.HasValue)
            {
                var frutaExiste = await _context.FrutasDelDiablo.AnyAsync(f=> f.Id == pirata.FrutaDelDiabloId.Value);
                if (!frutaExiste)
                {
                    throw new FrutaNoEncontradaException(pirata.FrutaDelDiabloId.Value);
                }
            }
            _context.Piratas.Add(pirata);
            await _context.SaveChangesAsync();
        }

        public async Task<Pirata?> UpdateAsync(int id, Pirata pirata)
        {
            if (pirata == null)
            {
                throw new ArgumentNullException(nameof(pirata), "El pirata no puede ser nulo.");
            }

            var pirataExistente = await _context.Piratas.FindAsync(id);
            if (pirataExistente == null)
            {
                throw new PirataNoEncontradoException(id);
            }

            if (pirata.FrutaDelDiabloId.HasValue)
            {
                var fruta = await _context.FrutasDelDiablo.AsNoTracking().FirstOrDefaultAsync(f=> f.Id == pirata.FrutaDelDiabloId.Value);
                if (fruta == null)
                {
                    throw new FrutaNoEncontradaException(pirata.FrutaDelDiabloId.Value);
                }
            }

            pirataExistente.Nombre = pirata.Nombre;
            pirataExistente.Descripcion = pirata.Descripcion;
            pirataExistente.Recompensa = pirata.Recompensa;
            pirataExistente.FrutaDelDiabloId = pirata.FrutaDelDiabloId;
            pirataExistente.TripulacionId = pirata.TripulacionId;
            await _context.SaveChangesAsync();
            return pirataExistente;
        }


        public async Task<bool> DeleteAsync(int id)
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