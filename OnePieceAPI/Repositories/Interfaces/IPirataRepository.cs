using OnePieceAPI.Models;

namespace OnePieceAPI.Services.Interfaces
{
    public interface IPirataRepository
    {
        Task<IEnumerable<Pirata>> GetAllAsync(int page, int pageSize);
        Task<Pirata?> GetAsync(int id);
        Task CreateAsync(Pirata pirata);
        Task<Pirata?> UpdateAsync(int id, Pirata pirata);
        Task<bool> DeleteAsync(int id);

    }
}
