using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Exceptions.FrutasDelDiablo;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Services
{
    public class FrutaDelDiabloService : IFrutaDelDiabloService
    {
        private readonly OnePieceContext _context;
        public FrutaDelDiabloService(OnePieceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<List<FrutaDelDiablo>> GetAllFrutasDelDiabloAsync()
        {
            return await _context.FrutasDelDiablo.OrderBy(f => f.Id).ToListAsync();
        }

        public async Task<FrutaDelDiablo?> GetFrutaDelDiabloAsync(int frutaId)
        {
            return await _context.FrutasDelDiablo.Where(f => f.Id == frutaId).FirstOrDefaultAsync();
        }

        public async Task CreateFrutaDelDiabloAsync(FrutaDelDiablo frutaDelDiablo)
        {
            if(frutaDelDiablo == null)
            {
                throw new ArgumentNullException(nameof(frutaDelDiablo));
            }
            //VERIFICAR QUE NO EXISTA UNA FRUTA CON EL MISMO NOMBRE
            _context.FrutasDelDiablo.Add(frutaDelDiablo);
            await _context.SaveChangesAsync();
        }

        public async Task<FrutaDelDiablo?> UpdateFrutaDelDiabloAsync(int frutaId, FrutaDelDiablo frutaDelDiablo)
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
        public async Task<bool> DeleteFrutaDelDiabloAsync(int frutaId)
        {
            var frutaExistente = await _context.FrutasDelDiablo.FindAsync(frutaId);
            if (frutaExistente == null)
            {
                return false;
            }
            /////////////VERIFICAR SI UN PIRATA LO TIENE
            _context.FrutasDelDiablo.Remove(frutaExistente);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
