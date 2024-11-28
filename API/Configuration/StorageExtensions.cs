using Microsoft.EntityFrameworkCore;
using DAL.Storage;
using DAL.Storage.Database;
using DAL.Storage.Files;

namespace API.Configuration;
public static class StorageExtensions
{
  public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
  {
    var dbaType = configuration["DBA"];

    if (dbaType == "db")
    {
      ConfigureDatabaseStorage(services, configuration);
    }
    else if (dbaType == "file")
    {
      ConfigureFileStorage(services);
    }
    else
    {
      throw new Exception("NO DBA IN CONFIGURATION");
    }
  }

  private static void ConfigureDatabaseStorage(IServiceCollection services, IConfiguration configuration)
  {
    var dbConfig = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<MyDbContext>(options =>
        options.UseNpgsql(dbConfig)
               .EnableSensitiveDataLogging());
    services.AddScoped<IStorage, PostgreSQLStorage>();
    Console.WriteLine("Running with Database storage");
  }

  private static void ConfigureFileStorage(IServiceCollection services)
  {
    services.AddScoped<IStorage, FilesStorage>();
    Console.WriteLine("Running with FilesStorage");
  }
}
