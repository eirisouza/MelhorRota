namespace Routes.Api.Models;

public record RouteModel
{
    public string Source { get; set; }
    public string Target { get; set; }
    public int Value { get; set; }
}
