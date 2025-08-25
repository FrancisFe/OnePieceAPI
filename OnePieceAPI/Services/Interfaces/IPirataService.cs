using OnePieceAPI.Models.Common;
using OnePieceAPI.Models.DTOs.Piratas;

namespace OnePieceAPI.Services.Interfaces
{
    public interface IPirataService
    {
        Task<PagedResult<PirataDto>> GetPirataConFiltrosAsync(PirataFiltrosDto pirataConFiltros);
        Task<PirataDto?> GetByIdAsync(int id);
        Task<PirataDto?> CreateAsync(CrearPirataDto pirata);
        Task<PirataDto?> UpdateAsync(int id, ActualizarPirataDto pirata);
        Task<bool> DeleteAsync(int id);
    }
}
