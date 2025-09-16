using Microsoft.EntityFrameworkCore;
using GameRouletteBackend.Roulette.Domain.Model.Aggregates;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration;

public static class RouletteGameConfiguration
{
    public static void ConfigureRouletteGame(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RouletteGame>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.GameId).IsRequired();
            entity.Property(e => e.WinningNumber).IsRequired(false);
            entity.Property(e => e.WinningColor).IsRequired(false).HasMaxLength(10);
            entity.Property(e => e.IsCompleted).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.CompletedAt).IsRequired(false);
            
            // Configurar índices
            entity.HasIndex(e => e.GameId).IsUnique();
            entity.HasIndex(e => e.CreatedAt);
            
            // Configurar tabla
            entity.ToTable("RouletteGames");
        });
    }
}
