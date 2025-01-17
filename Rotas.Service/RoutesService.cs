using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Repositories;
using Routes.Domain.Interfaces.Services;

namespace Routes.Service;

public sealed class RoutesService(IRouteRepository rotasRepository) : IRouteService
{
    private readonly IRouteRepository repository = rotasRepository;
    private List<Route>? routes;

    public async Task AddAsync(Route entity)
    {
        await repository.AddAsync(entity);
    }

    public async Task AddRangeAsync(List<Route> entities)
    {
        await repository.AddRangeAsync(entities);
    }

    public async Task<List<Route>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<List<Route>> GetBySourceAsync(string source)
    {
        return await repository.GetBySourceAsync(source);
    }

    public async Task<List<Route>> GetByTargetAsync(string target)
    {
        return await repository.GetByTargetAsync(target);
    }

    public async Task<string> GetBetterRouteAsync(string source, string target)
    {
        routes = await repository.GetAllAsync();
        List<string> betterRoute = [];
        var betteCost = BetterRoute([], betterRoute, source, target, 0);

        if (betteCost == int.MaxValue)
        {
            return "Rota não pode ser definida!";
        }

        return string.Join(" - ", betterRoute) + $" ao custo de ${betteCost}";
    }

    private int BetterRoute(HashSet<string> traveled, List<string> currentWay, string source, string target, int currentCoust)
    {
        if (source == target)
        {
            currentWay.Add(source);
            return currentCoust;
        }

        if (traveled.Contains(source))
            return int.MaxValue;

        traveled.Add(source);
        currentWay.Add(source);

        int lessCoust = int.MaxValue;

        var sourceRoutes = routes?.Where(r => r.Source == source).ToList();

        List<string> betterWay = [];

        foreach (var rota in sourceRoutes)
        {
            List<string> temporaryWay = new(currentWay); 
            int routeCoust = BetterRoute(new HashSet<string>(traveled), temporaryWay, rota.Target, target, currentCoust + rota.Value);

            if (routeCoust < lessCoust)
            {
                lessCoust = routeCoust;
                betterWay = temporaryWay;
            }
        }

        currentWay.RemoveAt(currentWay.Count - 1);

        if (lessCoust != int.MaxValue)
        {
            currentWay.Clear();
            currentWay.AddRange(betterWay);
        }

        return lessCoust;
    }
}
