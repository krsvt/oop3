using Microsoft.EntityFrameworkCore;
using shops.Storage;
using shops.Storage.Database;
using shops.Storage.Files;

namespace shops.Configuration;

public class StorageConfiguration
{

  public static void SetStorage(IConfiguration configuration,
      IServiceCollection services){
    var dbaType = configuration["DBA"];
    if (dbaType == "db")
    {
      var dbConfig = configuration["ConnectionStrings:DefaultConnection"];
      services.AddDbContext<MyDbContext>
        (options =>
         options.UseNpgsql(dbConfig));
      // I hope it can be treated as service
      services.AddScoped<IStorage, PostgreSQLStorage>();
    }
    else if (dbaType == "file")
    {
      services.AddScoped<IStorage, FilesStorage>();
    }
    else
      throw new Exception("NO DBA IN CONFIGURATION");

  }


}
