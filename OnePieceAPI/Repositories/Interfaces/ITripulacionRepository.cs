using OnePieceAPI.Models;

namespace OnePieceAPI.Repositories.Interfaces
{
    public interface ITripulacionRepository
    {
        Task<IEnumerable<Tripulacion>> GetAllTripulacionesAsync();
        Task<Tripulacion?> GetTripulacionByIdAsync(int id);
        Task CreateTripulacionAsync(Tripulacion tripulacion);
        Task<Tripulacion?> UpdateTripulacionAsync(int id, Tripulacion tripulacion);
        Task<bool> DeleteTripulacionAsync(int id);
    }
}
