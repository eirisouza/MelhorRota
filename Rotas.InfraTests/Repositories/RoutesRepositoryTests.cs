using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Repositories;
using Routes.Infra.Configuration;
using Xunit;

namespace Routes.Infra.Repositories.Tests;
public class RoutesRepositoryTests 
{
    private readonly RoutesDbContext contextMock;
    private readonly IRouteRepository routeRepository;

    public RoutesRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<RoutesDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        contextMock = new RoutesDbContext(options);

        routeRepository = new RouteRepository(contextMock);
    }

    [Fact]
    public async Task AddAsync_WhenSuccessInclud_ReturnSucces()
    {
        var route = new Route ("GRU", "CDG",  100);

        await routeRepository.AddAsync(route);

        await contextMock.Routes.Received(1).AddAsync(Arg.Any<Route>());
        await contextMock.SaveChangesAsync().Received(1);
    }

    [Fact]
    public async Task AddAsync_WhenRepositoryFail_ReturnException()
    {
        var route = new Route("GRU", "CDG", 100);

        contextMock.Routes.AddAsync(Arg.Any<Route>()).Throws(new System.Exception("Error"));

        await routeRepository.AddAsync(route);

        await contextMock.Routes.Received(1).AddAsync(Arg.Any<Route>());
        await contextMock.SaveChangesAsync().Received(1);
    }

    [Fact]
    public async Task AddRangeAsync_DeveAdicionarListaDeRotasComSucesso()
    {
        var routes = new List<Route>
        {
            new Route ("GRU", "CDG",  100),
            new Route ("SCL", "BRC", 50)
        };

        await routeRepository.AddRangeAsync(routes);

        await contextMock.Routes.Received(1).AddRangeAsync(Arg.Any<IEnumerable<Route>>());
        await contextMock.SaveChangesAsync().Received(1);
    }

    [Fact]
    public async Task GetBySourceAsync_DeveRetornarRotasCorretas()
    {
        var routes = new List<Route>
        {
            new Route ("GRU", "CDG", 100),
            new Route ("GRU", "SCL", 50)
        };

        contextMock.Routes.Where(x => x.Source == "GRU").Returns(routes.Where(x => x.Source == "GRU").AsQueryable());

        var result = await routeRepository.GetBySourceAsync("GRU");

        result.Should().HaveCount(2);
        result.First().Source.Should().Be("GRU");
        result.First().Target.Should().Be("CDG");
    }

    [Fact]
    public async Task GetBySourceAsync_DeveRetornarListaVazia_QuandoNaoEncontrarRotas()
    {
        contextMock.Routes.Where(x => x.Source == "GRU").Returns(new List<Route>().AsQueryable());

        var result = await routeRepository.GetBySourceAsync("GRU");

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByTargetAsync_DeveRetornarRotasCorretas()
    {
        var routes = new List<Route>
        {
            new Route ("GRU", "CDG", 100),
            new Route ("SCL", "CDG", 50)
        };

        contextMock.Routes.Where(x => x.Target == "CDG").Returns(routes.Where(x => x.Target == "CDG").AsQueryable());

        var result = await routeRepository.GetByTargetAsync("CDG");

        result.Should().HaveCount(2);
        result.First().Target.Should().Be("CDG");
        result.First().Source.Should().Be("GRU");
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarTodasAsRotas()
    {
        var routes = new List<Route>
        {
            new Route ("GRU", "CDG", 100),
            new Route ("SCL", "BRC", 50)
        };

        contextMock.Routes.ToListAsync().Returns(routes);

        var result = await routeRepository.GetAllAsync();

        result.Should().HaveCount(2);
    }
}