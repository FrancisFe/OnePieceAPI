using OnePieceAPI.Models;

namespace OnePieceAPI.Services.Interfaces
{
    public interface IPirataService
    {
        Task<IEnumerable<Pirata>> GetAllPiratasAsync(int page, int pageSize);
        Task<Pirata?> GetPirataAsync(int id);
        Task CreatePirataAsync(Pirata pirata);
        Task<Pirata?> UpdatePirataAsync(int id, Pirata pirata);
        Task<bool> DeletePirataAsync(int id);

    }
}
