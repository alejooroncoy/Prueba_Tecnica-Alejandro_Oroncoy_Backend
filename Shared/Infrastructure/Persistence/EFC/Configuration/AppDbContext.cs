using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Configuration.Extensions;
using GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Configuration;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the Learning Center Platform
/// </summary>
/// <param name="options">
///     The options for the database context
/// </param>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
   /// <summary>
   ///     On configuring the database context
   /// </summary>
   /// <remarks>
   ///     This method is used to configure the database context.
   ///     It also adds the created and updated date interceptor to the database context.
   /// </remarks>
   /// <param name="builder">
   ///     The option builder for the database context
   /// </param>
   protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

   /// <summary>
   ///     On creating the database model
   /// </summary>
   /// <remarks>
   ///     This method is used to create the database model for the application.
   /// </remarks>
   /// <param name="builder">
   ///     The model builder for the database context
   /// </param>
   protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Roulette Context
        builder.ApplyRouletteConfiguration();
        
        // IAM Context
        builder.ConfigureAccount();
        builder.ConfigureUser();
        
        // General Naming Convention for the database objects
        builder.UseSnakeCaseNamingConvention();
    }
}