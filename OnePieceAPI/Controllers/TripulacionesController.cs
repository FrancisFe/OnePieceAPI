﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnePieceAPI.DTOs.Tripulaciones;
using OnePieceAPI.Models;
using OnePieceAPI.Services.Interfaces;

namespace OnePieceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripulacionesController : ControllerBase
    {
        private readonly ITripulacionService _tripulacionService;
        public TripulacionesController(ITripulacionService tripulacionService)
        {
            _tripulacionService = tripulacionService;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripulacionDto?>>> GetAllTripulaciones()
        {
            var tripulaciones = await _tripulacionService.GetAllAsync();
            return Ok(tripulaciones);
        }

        [HttpGet("{tripulacionId}")]
        public async Task<ActionResult<TripulacionDto?>> GetTripulacion(int tripulacionId)
        {
            var tripulacion = await _tripulacionService.GetByIdAsync(tripulacionId);
            if (tripulacion == null)
            {
                return NotFound();
            }
            return Ok(tripulacion);
        }

        [HttpPost]
        public async Task<ActionResult<TripulacionDto>> CreateTripulacion(CrearTripulacionDto tripulacion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tripulacionDto = await _tripulacionService.CreateAsync(tripulacion);

            return CreatedAtAction(nameof(GetTripulacion), new { tripulacionId = tripulacionDto.Id }, tripulacionDto);
        }

        [HttpPut("{tripulacionId}")]
        public async Task<ActionResult<Tripulacion?>> UpdateTripulacion(int tripulacionId, ActualizarTripulacionDto tripulacion)
        {
            if (tripulacion == null || !ModelState.IsValid)
            {
                return BadRequest("Tripulacion no puede ser nula");
            }

            var tripulacionExistente = await _tripulacionService.UpdateAsync(tripulacionId, tripulacion);
            if (tripulacionExistente == null)
            {
                return NotFound();
            }
            return Ok(tripulacionExistente);
        }

        [HttpDelete("{tripulacionId}")]
        public async Task<ActionResult<bool>> DeleteTripulacion(int tripulacionId)
        {
            var resultado = await _tripulacionService.DeleteAsync(tripulacionId);
            if (!resultado)
            {
                return NotFound($"No se encontró la tripulación con ID {tripulacionId}.");
            }
            return NoContent();
        }

    }
}
