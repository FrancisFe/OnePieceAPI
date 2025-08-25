using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Repositories.Interfaces
{
    public interface IPirataRepository
    {
        IQueryable<Pirata> GetQueryable();
        Task<Pirata?> GetAsync(int id);
        Task CreateAsync(Pirata pirata);
        Task<Pirata?> UpdateAsync(int id, Pirata pirata);
        Task<bool> DeleteAsync(int id);

    }
}
