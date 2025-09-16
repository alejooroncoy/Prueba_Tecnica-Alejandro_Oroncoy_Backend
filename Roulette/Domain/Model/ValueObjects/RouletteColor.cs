namespace GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

public static class RouletteColorValue
{
    public const string RED = "ROJO";
    public const string BLACK = "NEGRO";
    
    public static readonly string[] ValidColors = { RED, BLACK };
    
    public static bool IsValid(string color)
    {
        return ValidColors.Contains(color?.ToUpper());
    }
    
    public static string Validate(string color)
    {
        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("El color no puede estar vacío");
            
        var upperColor = color.ToUpper();
        if (!IsValid(upperColor))
            throw new ArgumentException($"Color inválido: {color}. Colores válidos: {string.Join(", ", ValidColors)}");
            
        return upperColor;
    }
    
    public static string GetColorForNumber(int number)
    {
        RouletteNumber.Validate(number);
        
        // Números rojos en la ruleta
        var redNumbers = new[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        
        if (number == 0)
            throw new ArgumentException("El número 0 no tiene color");
            
        return redNumbers.Contains(number) ? RED : BLACK;
    }
    
    public static string GenerateRandom()
    {
        var random = new Random();
        return ValidColors[random.Next(ValidColors.Length)];
    }
}
