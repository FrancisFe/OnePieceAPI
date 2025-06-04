using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Services
{
    public class PirataService : IPirataService
    {
        private readonly OnePieceContext _context;
        public PirataService(OnePieceContext context)
        {
           _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Pirata>> GetAllPiratasAsync()
        {
            return await _context.Piratas.OrderBy(p => p.Nombre).ToListAsync();
        }
        public async Task<Pirata?> GetPirataAsync(int pirataId)
        {
            return await _context.Piratas.Where(p => p.Id == pirataId).FirstOrDefaultAsync();
        }
        public async Task CreatePirataAsync(Pirata pirata)
        {
            if (pirata == null)
            {
                throw new ArgumentNullException(nameof(pirata));
            }
            if (pirata.FrutaDelDiabloId.HasValue)
            {
                var fruta = await _context.FrutasDelDiablo.FindAsync(pirata.FrutaDelDiabloId.Value);
                if (fruta == null)
                {
                    throw new Exception("La fruta del diablo especificada no existe.");
                }
            }
            _context.Piratas.Add(pirata);
            await _context.SaveChangesAsync();
        }
        public async Task<Pirata?> UpdatePirataAsync(int id, Pirata pirata)
        {

            if (pirata == null)
            {
                throw new ArgumentNullException(nameof(pirata));
            }
            var pirataExistente = await _context.Piratas.FindAsync(id);
            if (pirataExistente == null)
            {
                return null;
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

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

       
    }
}
