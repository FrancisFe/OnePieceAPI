using OnePieceAPI.Models;

namespace OnePieceAPI.Services.Interfaces
{
    public interface IFrutaDelDiabloService
    {
        Task<List<FrutaDelDiablo>> GetAllFrutasDelDiabloAsync();
        Task<FrutaDelDiablo?> GetFrutaDelDiabloAsync(int id);
        Task CreateFrutaDelDiabloAsync(FrutaDelDiablo frutaDelDiablo);
        Task<FrutaDelDiablo?> UpdateFrutaDelDiabloAsync(int id, FrutaDelDiablo frutaDelDiablo);
        Task<bool> DeleteFrutaDelDiabloAsync(int id);

    }
}
