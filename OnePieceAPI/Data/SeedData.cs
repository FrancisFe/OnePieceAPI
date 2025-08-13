using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models;
using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(OnePieceContext context)
        {
            // FRUTAS DEL DIABLO
            if (!context.FrutasDelDiablo.Any())
            {
                var frutas = new List<FrutaDelDiablo>
                {
                    new FrutaDelDiablo { Nombre = "Gomu Gomu no Mi", Tipo = "Paramecia", Descripcion = "Convierte el cuerpo en goma." },
                    new FrutaDelDiablo { Nombre = "Mera Mera no Mi", Tipo = "Logia", Descripcion = "Permite controlar el fuego." }
                };
                context.FrutasDelDiablo.AddRange(frutas);
                context.SaveChanges();
            }

            // TRIPULACIONES
            if (!context.Tripulaciones.Any())
            {
                var sombreroDePaja = new Tripulacion
                {
                    Nombre = "Sombrero de Paja",
                    RecompensaTotal = 0
                };

                var barbaBlanca = new Tripulacion
                {
                    Nombre = "Piratas de Barba Blanca",
                    RecompensaTotal = 0
                };

                context.Tripulaciones.AddRange(sombreroDePaja, barbaBlanca);
                context.SaveChanges();
            }

            // PIRATAS
            if (!context.Piratas.Any())
            {
                var gomuId = context.FrutasDelDiablo.First(f => f.Nombre == "Gomu Gomu no Mi").Id;
                var meraId = context.FrutasDelDiablo.First(f => f.Nombre == "Mera Mera no Mi").Id;

                var sombreroId = context.Tripulaciones.First(t => t.Nombre == "Sombrero de Paja").Id;
                var barbaBlancaId = context.Tripulaciones.First(t => t.Nombre == "Piratas de Barba Blanca").Id;

                var piratas = new List<Pirata>
                {
                    new Pirata
                    {
                        Nombre = "Monkey D. Luffy",
                        Recompensa = 150000000,
                        FrutaDelDiabloId = gomuId,
                        TripulacionId = sombreroId
                    },
                    new Pirata
                    {
                        Nombre = "Portgas D. Ace",
                        Recompensa = 550000000,
                        FrutaDelDiabloId = meraId,
                        TripulacionId = barbaBlancaId
                    },
                    new Pirata
                    {
                        Nombre = "Roronoa Zoro",
                        Recompensa = 320000000,
                        FrutaDelDiabloId = null,
                        TripulacionId = sombreroId
                    }
                };

                context.Piratas.AddRange(piratas);
                context.SaveChanges();
            }
        }
    }
}
