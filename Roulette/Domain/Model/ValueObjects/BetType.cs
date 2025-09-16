namespace GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

public static class BetTypeValue
{
    public const string COLOR = "COLOR";
    public const string NUMBER = "NUMERO";
    public const string EVEN_ODD = "PAR_IMPAR";
    
    public static readonly string[] ValidBetTypes = { COLOR, NUMBER, EVEN_ODD };
    
    public static bool IsValid(string betType)
    {
        return ValidBetTypes.Contains(betType?.ToUpper());
    }
    
    public static string Validate(string betType)
    {
        if (string.IsNullOrWhiteSpace(betType))
            throw new ArgumentException("El tipo de apuesta no puede estar vacío");
            
        var upperBetType = betType.ToUpper();
        if (!IsValid(upperBetType))
            throw new ArgumentException($"Tipo de apuesta inválido: {betType}. Tipos válidos: {string.Join(", ", ValidBetTypes)}");
            
        return upperBetType;
    }
}
