namespace GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

public static class RouletteNumber
{
    public const int MIN_VALUE = 0;
    public const int MAX_VALUE = 36;
    
    public static readonly int[] ValidNumbers = Enumerable.Range(MIN_VALUE, MAX_VALUE + 1).ToArray();
    
    public static bool IsValid(int number)
    {
        return number >= MIN_VALUE && number <= MAX_VALUE;
    }
    
    public static int Validate(int number)
    {
        if (!IsValid(number))
            throw new ArgumentException($"Número de ruleta inválido: {number}. Debe estar entre {MIN_VALUE} y {MAX_VALUE}");
            
        return number;
    }
    
    public static int GenerateRandom()
    {
        var random = new Random();
        return random.Next(MIN_VALUE, MAX_VALUE + 1);
    }
}
