using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models;

namespace OnePieceAPI.Data
{
    public class OnePieceContext : DbContext
    {
        public OnePieceContext(DbContextOptions<OnePieceContext> options) : base(options)
        { }
        public DbSet<Pirata> Piratas { get; set; }
        public DbSet<FrutaDelDiablo> FrutasDelDiablo { get; set; }
        public DbSet<Tripulacion> Tripulaciones { get; set; }
    }
}
