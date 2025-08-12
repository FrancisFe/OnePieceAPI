using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Models;
using OnePieceAPI.Repositories.Interfaces;

namespace OnePieceAPI.Repositories
{
    public class TripulacionRepository : ITripulacionRepository
    {
        private readonly OnePieceContext _context;
        public TripulacionRepository(OnePieceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Tripulacion>> GetAllAsync()
        {
            return await _context.Tripulaciones.OrderBy(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<Tripulacion?> GetByIdAsync(int id)
        {
            return await _context.Tripulaciones
                .Include(t => t.Capitan)
                .Include(t => t.Miembros)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task CreateAsync(Tripulacion tripulacion)
        {
            if(tripulacion == null)
            {
                throw new ArgumentNullException(nameof(tripulacion), "La tripulación no puede ser nula");
            }
            _context.Tripulaciones.Add(tripulacion);
            await _context.SaveChangesAsync();
        }

        public async Task<Tripulacion?> UpdateAsync(int id, Tripulacion tripulacion)
        {
            if(tripulacion == null)
            {
                throw new ArgumentNullException(nameof(tripulacion), "La tripulacion no puede ser nula");
            }
            var tripulacionExistente = await _context.Tripulaciones.FindAsync(id);
            if(tripulacionExistente == null)
            {
                return null;
            }
            tripulacionExistente.Nombre = tripulacion.Nombre;
            tripulacionExistente.Descripcion = tripulacion.Descripcion;
            tripulacionExistente.CapitanId = tripulacion.CapitanId;
            _context.Tripulaciones.Update(tripulacionExistente);
            await _context.SaveChangesAsync();
            return tripulacionExistente;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var tripulacion = await _context.Tripulaciones.FindAsync(id);
            if(tripulacion == null)
            {
                return false;
            }
            _context.Tripulaciones.Remove(tripulacion);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
