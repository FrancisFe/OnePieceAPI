using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Models.Entities;

namespace OnePieceAPI.Data;

public class OnePieceContext(DbContextOptions<OnePieceContext> options) : DbContext(options)
{
    public DbSet<Pirata> Piratas { get; set; }
    public DbSet<FrutaDelDiablo> FrutasDelDiablo { get; set; }
    public DbSet<Tripulacion> Tripulaciones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pirata>()
            .HasOne(p => p.Tripulacion)
            .WithMany(t => t.Miembros)
            .HasForeignKey(p => p.TripulacionId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Tripulacion>()
            .HasOne(t => t.Capitan)
            .WithMany()
            .HasForeignKey(t => t.CapitanId)
            .OnDelete(DeleteBehavior.SetNull);

        base.OnModelCreating(modelBuilder);
    }
}