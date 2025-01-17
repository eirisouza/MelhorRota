using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Routes.Domain.Entities;

public sealed record Route
{
    public Route() { }

    public Route(string source, string target, int value)
    {
        Source = source;
        Target = target;
        Value = value;
    }
    [Key, Column(Order = 1)]
    public string Source { get; private set; }
    [Key, Column(Order = 2)]
    public string Target { get; private set; }
    public int Value { get; private set; }

    public void AlterValue(int value)
    {
        Value = value;
    }
}
