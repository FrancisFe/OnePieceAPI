﻿using OnePieceAPI.DTOs.Tripulaciones;
using OnePieceAPI.Models;

namespace OnePieceAPI.Services.Interfaces
{
    public interface ITripulacionService
    {
        Task<IEnumerable<TripulacionDto>> GetAllAsync();
        Task<TripulacionDto?> GetByIdAsync(int id);
        Task<TripulacionDto> CreateAsync(CrearTripulacionDto tripulacion);
        Task<TripulacionDto?> UpdateAsync(int id, ActualizarTripulacionDto tripulacion);
        Task<bool> DeleteAsync(int id);
        Task<TripulacionDto?> AddPirataAsync(int tripulacionId, int pirataId);
        Task<TripulacionDto?> RemovePirataAsync(int tripulacionId, int pirataId);
        Task UpdateRecompensaTotalAsync(int tripulacionId);
    }
}
