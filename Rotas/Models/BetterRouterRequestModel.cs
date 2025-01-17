using Microsoft.AspNetCore.Mvc;

namespace Routes.Api.Models;

public sealed record BetterRouterRequestModel
{
    public string Source { get; set; }
    public string Target { get; set; }
}
