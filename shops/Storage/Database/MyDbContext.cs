using shops.Entities;
using Microsoft.EntityFrameworkCore;

namespace shops.Storage.Database;

public class MyDbContext : DbContext
{
  public MyDbContext(DbContextOptions<MyDbContext> options)
      : base(options)
  {
  }

  public DbSet<Product> Songs { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // modelBuilder.Entity<Song>()
    //   .HasMany(s => s.SongsCollections)
    //   .WithMany(sc => sc.Songs)
    //   .UsingEntity(j => j.ToTable("song_songs_collection"));
    //
    // // Определяем ArtistSearchResultDto как безключевую сущность
    // modelBuilder.Entity<ArtistSearchResultDto>()
    //   .HasNoKey();
    //
    // modelBuilder.Entity<AlbumAndCollectionSearchResultDto>()
    //   .HasNoKey();

  }
}
