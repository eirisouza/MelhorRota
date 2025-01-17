namespace Routes.Domain.Constants;

public static class RoutesValidatorMessages
{
    public const string SourceValueInvalid = "O campo origem é inválido. Valores válidos Ex: AAA, BGT, ABC.";
    public const string TargetValueInvalid = "O campo destino é inválido. Valores válidos Ex: AAA, BGT, ABC.";
    public const string TargetCanotEgualSource = "O campo destino e campo origem não podem conter os mesmo valores.";
    public const string ValueIvalid = "O campo valor é inválido.";
}
