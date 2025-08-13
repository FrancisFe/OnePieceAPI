using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Services.Interfaces
{
    public interface IFrutaDelDiabloRepository
    {
        Task<List<FrutaDelDiablo>> GetAllAsync(int page, int pageSize);
        Task<FrutaDelDiablo?> GetAsync(int id);
        Task CreateAsync(FrutaDelDiablo frutaDelDiablo);
        Task<FrutaDelDiablo?> UpdateAsync(int id, FrutaDelDiablo frutaDelDiablo);
        Task<bool> DeleteAsync(int id);

    }
}
