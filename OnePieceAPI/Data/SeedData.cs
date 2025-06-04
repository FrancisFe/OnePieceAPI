using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models;

namespace OnePieceAPI.Data
{
    public static class SeedData
    {
        public static void Inicializar(OnePieceContext context)
        {
            // Aplica migraciones pendientes
            context.Database.Migrate();

            // Si ya hay piratas, no hace nada
            if (context.Piratas.Any())
                return;

            // Seed de frutas del diablo
            var frutas = new List<FrutaDelDiablo>
            {
                new FrutaDelDiablo { Nombre = "Gomu Gomu no Mi", Tipo = "Paramecia", Descripcion = "Convierte el cuerpo en goma." },
                new FrutaDelDiablo { Nombre = "Mera Mera no Mi", Tipo = "Logia", Descripcion = "Permite controlar el fuego." },
                new FrutaDelDiablo { Nombre = "Hie Hie no Mi", Tipo = "Logia", Descripcion = "Permite congelar todo." }
            };

            context.FrutasDelDiablo.AddRange(frutas);
            context.SaveChanges();

            // Seed de piratas
            var piratas = new List<Pirata>
            {
                new Pirata {
                    Nombre = "Monkey D. Luffy",
                    Descripcion = "Capitán de los Sombrero de Paja.",
                    Recompensa = 300000000,
                    FrutaDelDiabloId = frutas[0].Id
                },
                new Pirata {
                    Nombre = "Portgas D. Ace",
                    Descripcion = "Comandante de los Piratas de Barbablanca.",
                    Recompensa = 550000000,
                    FrutaDelDiabloId = frutas[1].Id
                },
                new Pirata {
                    Nombre = "Aokiji",
                    Descripcion = "Ex-Almirante de la Marina.",
                    Recompensa = 800000000,
                    FrutaDelDiabloId = frutas[2].Id
                }
            };

            context.Piratas.AddRange(piratas);
            context.SaveChanges();
        }
    }
}