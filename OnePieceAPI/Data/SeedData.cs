using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models;

namespace OnePieceAPI.Data
{
    public static class SeedData
    {
        public static void Initialize(OnePieceContext context)
        {
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

            if (!context.Piratas.Any())
            {
                var luffy = new Pirata
                {
                    Nombre = "Monkey D. Luffy",
                    Recompensa = 150000000,
                    FrutaDelDiabloId = context.FrutasDelDiablo.FirstOrDefault(f => f.Nombre == "Gomu Gomu no Mi")?.Id
                };

                var ace = new Pirata
                {
                    Nombre = "Portgas D. Ace",
                    Recompensa = 550000000,
                    FrutaDelDiabloId = context.FrutasDelDiablo.FirstOrDefault(f => f.Nombre == "Mera Mera no Mi")?.Id
                };

                var zoro = new Pirata
                {
                    Nombre = "Roronoa Zoro",
                    Recompensa = 320000000,
                    FrutaDelDiabloId = null // No posee una fruta del diablo
                };

                context.Piratas.AddRange(luffy, ace, zoro);
                context.SaveChanges();
            }
        }

    }
}