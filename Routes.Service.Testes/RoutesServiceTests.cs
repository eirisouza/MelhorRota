using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Repositories;
using FluentAssertions;

namespace Routes.Service.Testes;
public sealed class RoutesServiceTests
{
    private readonly IRouteRepository routeRepositoryMock;
    private readonly RoutesService routesService;

    public RoutesServiceTests()
    {
        routeRepositoryMock = Substitute.For<IRouteRepository>();
        routesService = new RoutesService(routeRepositoryMock);
    }

    [Fact]
    public async Task AddAsync_WhenRepositoryConclude_Success()
    {
        var route = new Route("GRU", "CDG", 100);
        await routesService.AddAsync(route);

        await routeRepositoryMock.Received(1).AddAsync(route);
    }

    [Fact]
    public async Task AddAsync_WhenRepositoryFail_Failure()
    {
        var route = new Route("GRU", "CDG", 100);
        routeRepositoryMock.AddAsync(Arg.Any<Route>()).Throws(new Exception("Error"));

        await routesService.Invoking(service => service.AddAsync(route))
            .Should().ThrowAsync<Exception>().WithMessage("Error");
    }

    [Fact]
    public async Task AddRangeAsync_WhenRepositoryConclude_Success()
    {
        var routes = new List<Route>
        {
            new("GRU", "CDG", 100),
            new("SCL", "BRC", 50)
        };
        await routesService.AddRangeAsync(routes);

        await routeRepositoryMock.Received(1).AddRangeAsync(routes);
    }

    [Fact]
    public async Task AddRangeAsync_WhenRepositoryFail_Failure()
    {
        var routes = new List<Route>
        {
            new("GRU", "CDG", 100)
        };
        routeRepositoryMock.AddRangeAsync(Arg.Any<List<Route>>()).Throws(new Exception("Error"));

        var response = routesService.Invoking(service => service.AddRangeAsync(routes));

        await response.Should().ThrowAsync<Exception>().WithMessage("Error");
    }

    [Fact]
    public async Task GetAllAsync_WhenFound_Success()
    {
        var routes = new List<Route>
        {
            new("GRU", "CDG", 100),
            new("SCL", "BRC", 50)
        };

        routeRepositoryMock.GetAllAsync().Returns(Task.FromResult(routes));
        var result = await routesService.GetAllAsync();

        result.Should().HaveCount(2);
        result.First().Source.Should().Be("GRU");
    }

    [Fact]
    public async Task GetBySourceAsync_WhenFound_Success()
    {
        var routes = new List<Route>
        {
            new("GRU", "CDG", 100),
            new("GRU", "SCL", 50)
        };
        routeRepositoryMock.GetBySourceAsync("GRU").Returns(Task.FromResult(routes));
        var result = await routesService.GetBySourceAsync("GRU");

        result.Should().HaveCount(2);
        result.All(r => r.Source == "GRU").Should().BeTrue();
    }

    [Fact]
    public async Task GetBySourceAsync_WhenNotFound_Failure()
    {
        routeRepositoryMock.GetBySourceAsync("XYZ").Returns(Task.FromResult(new List<Route>()));
        var result = await routesService.GetBySourceAsync("XYZ");
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByTargetAsync_WhenFound_Success()
    {
        var routes = new List<Route>
        {
            new("GRU", "CDG", 100),
            new("SCL", "CDG", 50)
        };
        routeRepositoryMock.GetByTargetAsync("CDG").Returns(Task.FromResult(routes));
        var result = await routesService.GetByTargetAsync("CDG");
        result.Should().HaveCount(2);
        result.All(r => r.Target == "CDG").Should().BeTrue();
    }

    [Fact]
    public async Task GetBetteRouteAsync_WhenFoundAllRoutes_Success()
    {
        var routes = new List<Route>
        {
            new("GRU", "BRC", 10),
            new("BRC", "SCL", 5),
            new("SCL", "ORL", 20),
            new("GRU", "SCL", 50),
            new("ORL", "CDG", 5),
            new("SCL", "CDG", 30)
        };

        routeRepositoryMock.GetAllAsync().Returns(Task.FromResult(routes));

        var result = await routesService.GetBetterRouteAsync("GRU", "CDG");

        result.Should().Be("GRU - BRC - SCL - ORL - CDG ao custo de $40");
    }

    [Fact]
    public async Task GetBetteRouteAsync_WhenNotFoundRoute_Failure()
    {
        routeRepositoryMock.GetAllAsync().Returns(Task.FromResult(new List<Route>()));

        var result = await routesService.GetBetterRouteAsync("GRU", "CDG");

        result.Should().Be("Rota não pode ser definida!");
    }
}