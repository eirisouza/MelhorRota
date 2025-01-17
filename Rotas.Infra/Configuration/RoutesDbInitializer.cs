using Routes.Domain.Entities;

namespace Routes.Infra.Configuration;

public static class RoutesDbInitializer
{
    public static void Initialize(RoutesDbContext context)
    {
        if (!context.Routes.Any())
        {
            context.Routes.AddRange(
                new Route("GRU", "BRC", 10),
                new Route("BRC", "SCL", 5),
                new Route("GRU", "CDG", 75),
                new Route("GRU", "SCL", 20),
                new Route("GRU", "ORL", 56),
                new Route("ORL", "CDG", 5),
                new Route("SCL", "ORL", 20)
            );
            context.SaveChanges();
        }
    }
}
