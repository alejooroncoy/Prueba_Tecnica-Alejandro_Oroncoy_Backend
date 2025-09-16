using Microsoft.EntityFrameworkCore;
using GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class RouletteConfigurationExtensions
{
    public static void ApplyRouletteConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.ConfigureRouletteGame();
        modelBuilder.ConfigureBet();
        modelBuilder.ConfigureUser();
    }
}
