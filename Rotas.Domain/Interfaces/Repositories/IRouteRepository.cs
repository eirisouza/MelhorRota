using Routes.Domain.Entities;

namespace Routes.Domain.Interfaces.Repositories;

public interface IRouteRepository
{
    Task AddAsync(Route entity);
    Task AddRangeAsync(List<Route> entities);
    Task<List<Route>> GetBySourceAsync(string source);
    Task<List<Route>> GetByTargetAsync(string target);
    Task<List<Route>> GetAllAsync();
}
