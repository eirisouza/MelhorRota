using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Repositories;
using Routes.Infra.Configuration;

namespace Routes.Infra.Repositories;

public sealed class RouteRepository(RoutesDbContext context) : IRouteRepository
{
    private readonly RoutesDbContext context = context;

    public async Task AddAsync(Route entity)
    {
		var transaction = context.Database.BeginTransaction();
		try
		{
            await context.Routes.AddAsync(entity);
            await transaction.CommitAsync();
        }
		catch
		{
			await transaction.RollbackAsync();
		}

        await context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Route> entities)
    {
        var transaction = context.Database.BeginTransaction();
        try
        {
            await context.Routes.AddRangeAsync(entities);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }

        await context.SaveChangesAsync();
    }

    public async Task<List<Route>> GetBySourceAsync(string source)
    {
        return await context.Routes.Where(x => x.Source == source).ToListAsync();
    }

    public async Task<List<Route>> GetByTargetAsync(string target)
    {
        return await context.Routes.Where(x => x.Target == target).ToListAsync();
    }

    public async Task<List<Route>> GetAllAsync()
    {
        return await context.Routes.ToListAsync();
    }

}
