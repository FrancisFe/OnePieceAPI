using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Exceptions.FrutasDelDiablo;
using OnePieceAPI.Models.Entities;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Repositories
{
    public class FrutaDelDiabloRepository : IFrutaDelDiabloRepository
    {
        private readonly OnePieceContext _context;
        public FrutaDelDiabloRepository(OnePieceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<FrutaDelDiablo>> GetAllAsync(int page, int pageSize)
        {
            return await _context.FrutasDelDiablo
                .AsNoTracking()
                .OrderBy(f => f.Id)
                .Skip((page-1)*pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<FrutaDelDiablo?> GetAsync(int frutaId)
        {
            return await _context.FrutasDelDiablo
                .AsNoTracking()
                .Where(f => f.Id == frutaId)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(FrutaDelDiablo frutaDelDiablo)
        {
            if (frutaDelDiablo == null)
            {
                throw new ArgumentNullException(nameof(frutaDelDiablo));
            }
            var frutaExistente = await _context.FrutasDelDiablo
                .FirstOrDefaultAsync(f => f.Nombre.ToLower() == frutaDelDiablo.Nombre.ToLower());
           if(frutaExistente != null)
            {
                throw new FrutaYaExisteException(frutaDelDiablo.Nombre);
            }
            _context.FrutasDelDiablo.Add(frutaDelDiablo);
            await _context.SaveChangesAsync();
        }

        public async Task<FrutaDelDiablo?> UpdateAsync(int frutaId, FrutaDelDiablo frutaDelDiablo)
        {
            if(frutaDelDiablo == null)
            {
                throw new FrutaNoEncontradaException();
            }
            var frutaExistente = await _context.FrutasDelDiablo.FindAsync(frutaId);
            if(frutaExistente == null)
            {
                throw new FrutaNoEncontradaException(frutaId);
            }
            // Actualizar los campos de la fruta existente
            frutaExistente.Nombre = frutaDelDiablo.Nombre;
            frutaExistente.Tipo = frutaDelDiablo.Tipo;
            frutaExistente.Descripcion = frutaDelDiablo.Descripcion;
            _context.FrutasDelDiablo.Update(frutaExistente);
            await _context.SaveChangesAsync();
            return frutaExistente;
        }
        public async Task<bool> DeleteAsync(int frutaId)
        {
            var frutaExistente = await _context.FrutasDelDiablo.FindAsync(frutaId);
            if (frutaExistente == null)
            {
                return false;
            }
            var pirataConFruta = await _context.Piratas
                .FirstOrDefaultAsync(p => p.FrutaDelDiabloId == frutaId);
            if(pirataConFruta != null)
            {
                throw new InvalidOperationException("No se puede eliminar la fruta porque está asignada a un pirata");
            }
                _context.FrutasDelDiablo.Remove(frutaExistente);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
