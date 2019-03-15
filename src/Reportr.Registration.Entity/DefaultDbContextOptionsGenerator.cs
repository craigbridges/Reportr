namespace Reportr.Registration.Entity
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.IO;

    /// <summary>
    /// Represents the default database context options generator
    /// </summary>
    public sealed class DefaultDbContextOptionsGenerator : IDbContextOptionsGenerator
    {
        /// <summary>
        /// Generates the context options
        /// </summary>
        /// <returns>The database context options</returns>
        public DbContextOptions Generate()
        {
            var builder = new DbContextOptionsBuilder();
            var directory = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
               .SetBasePath(directory)
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString
            (
                "ReportrDbContext"
            );

            if (connectionString == null)
            {
                throw new InvalidOperationException
                (
                    "The ReportrDbContext connection string has not been configured."
                );
            }

            builder.UseLazyLoadingProxies();
            builder.UseSqlServer(connectionString);

            return builder.Options;
        }
    }
}
