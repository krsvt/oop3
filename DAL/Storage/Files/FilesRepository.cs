using System.Linq.Expressions;
using System.Text.Json;
using DAL.Entities;

namespace DAL.Storage.Files;

public class FilesRepository<T> : IRepository<T> where T : BaseEntity
{
  private readonly string _filePath;

  public FilesRepository(string filePath)
  {
    _filePath = filePath;
  }

  public async Task<List<T>> GetAllAsync()
  {
    if (!File.Exists(_filePath))
      return new List<T>();

    var json = await File.ReadAllTextAsync(_filePath);
    return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
  }

  public async Task<T> GetAsync(int id)
  {
    var entities = await GetAllAsync();
    return entities.FirstOrDefault(e => e.Id == id);
  }

  public async Task AddAsync(T entity)
  {
    var entities = await GetAllAsync();
    entities.Add(entity);
    await SaveAsync(entities);
  }

  public async Task<List<T>> GetByIdsAsync(List<int> ids)
  {
    var entities = await GetAllAsync();
    return entities.Where(e => ids.Contains(e.Id)).ToList();
  }

  public async Task AddAllAsync(List<T> entities)
  {
    var existingEntities = await GetAllAsync();
    existingEntities.AddRange(entities);
    await SaveAsync(existingEntities);
  }

  private async Task SaveAsync(List<T> entities)
  {
    var json = JsonSerializer.Serialize(entities, new JsonSerializerOptions { WriteIndented = true });
    await File.WriteAllTextAsync(_filePath, json);
  }

  public async Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
  {
    var entities = await GetAllAsync();
    var filteredEntities = entities.AsQueryable().Where(predicate).ToList();
    return filteredEntities;
  }
}
