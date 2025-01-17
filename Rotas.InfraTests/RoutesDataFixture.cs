using Microsoft.EntityFrameworkCore;
using Routes.Domain.Entities;
using Routes.Infra.Configuration;

namespace Routes.InfraTests;

public class RoutesDataFixture : IDisposable
{
    public RoutesDbContext RoutesContext { get; private set; }

    public RoutesDataFixture()
    {
        var options = new DbContextOptionsBuilder<RoutesDbContext>()
            .UseInMemoryDatabase("MovieListDatabase")
            .Options;

        RoutesContext = new RoutesDbContext(options);

        RoutesContext.Routes.Add(new Route("GRU", "BRC", 10));
        RoutesContext.Routes.Add(new Route("BRC", "SCL", 5));
        RoutesContext.Routes.Add(new Route("GRU", "CDG", 75));
        RoutesContext.Routes.Add(new Route("GRU", "SCL", 20));
        RoutesContext.Routes.Add(new Route("GRU", "ORL", 56));
        RoutesContext.Routes.Add(new Route("ORL", "CDG", 5));
        RoutesContext.Routes.Add(new Route("SCL", "ORL", 20));

        RoutesContext.SaveChanges();
    }

    public void Dispose()
    {
        RoutesContext.Dispose();
    }
}
