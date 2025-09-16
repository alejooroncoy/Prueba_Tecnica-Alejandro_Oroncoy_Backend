using Microsoft.EntityFrameworkCore;
using GameRouletteBackend.Roulette.Domain.Model.Aggregates;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration;

public static class BetConfiguration
{
    public static void ConfigureBet(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.BetId).IsRequired();
            entity.Property(e => e.GameId).IsRequired();
            entity.Property(e => e.UserUid).IsRequired();
            entity.Property(e => e.BetType).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.Number).IsRequired(false);
            entity.Property(e => e.Color).IsRequired(false).HasMaxLength(10);
            entity.Property(e => e.EvenOdd).IsRequired(false).HasMaxLength(10);
            entity.Property(e => e.IsWinning).IsRequired(false);
            entity.Property(e => e.WinningsAmount).IsRequired(false).HasColumnType("decimal(18,2)");
            entity.Property(e => e.CreatedAt).IsRequired();
            
            // Configurar índices
            entity.HasIndex(e => e.BetId).IsUnique();
            entity.HasIndex(e => e.GameId);
            entity.HasIndex(e => e.UserUid);
            entity.HasIndex(e => e.CreatedAt);
            
            // Configurar tabla
            entity.ToTable("Bets");
        });
    }
}
