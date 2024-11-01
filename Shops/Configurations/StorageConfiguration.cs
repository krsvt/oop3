using Microsoft.EntityFrameworkCore;
using Shops.Storage;
using Shops.Storage.Database;
using Shops.Storage.Files;

namespace Shops.Configuration;

public class StorageConfiguration
{

  public static void SetStorage(IConfiguration configuration,
      IServiceCollection services){
    var dbaType = configuration["DBA"];
    if (dbaType == "db")
    {
      var dbConfig = configuration["ConnectionStrings:DefaultConnection"];
      services.AddDbContext<MyDbContext>
        (options => options
         .UseNpgsql(dbConfig)
         .EnableSensitiveDataLogging());
      // I hope it can be treated as a service
      services.AddScoped<IStorage, PostgreSQLStorage>();
      Console.WriteLine("Runnning with PostgresStorage");
    }
    else if (dbaType == "file")
    {
      services.AddScoped<IStorage, FilesStorage>();
      Console.WriteLine("Runnning with files storage");
    }
    else
      throw new Exception("NO DBA IN CONFIGURATION");

  }


}
