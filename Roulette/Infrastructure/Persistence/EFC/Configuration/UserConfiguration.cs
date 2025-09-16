using Microsoft.EntityFrameworkCore;
using GameRouletteBackend.IAM.Domain.Model.Aggregates;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration;

public static class UserConfiguration
{
    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18,2)");
            
            // Configurar índices
            entity.HasIndex(e => e.Name).IsUnique();
            
            // Configurar tabla
            entity.ToTable("Users");
        });
    }
}
