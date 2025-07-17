using OnePieceAPI.Models;

namespace OnePieceAPI.Repositories.Interfaces
{
    public interface ITripulacionRepository
    {
        Task<IEnumerable<Tripulacion>> GetAllAsync();
        Task<Tripulacion?> GetByIdAsync(int id);
        Task CreateAsync(Tripulacion tripulacion);
        Task<Tripulacion?> UpdateAsync(int id, Tripulacion tripulacion);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
