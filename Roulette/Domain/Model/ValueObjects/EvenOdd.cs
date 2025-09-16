namespace GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

public static class EvenOddValue
{
    public const string EVEN = "PAR";
    public const string ODD = "IMPAR";
    
    public static readonly string[] ValidEvenOdd = { EVEN, ODD };
    
    public static bool IsValid(string evenOdd)
    {
        return ValidEvenOdd.Contains(evenOdd?.ToUpper());
    }
    
    public static string Validate(string evenOdd)
    {
        if (string.IsNullOrWhiteSpace(evenOdd))
            throw new ArgumentException("El valor par/impar no puede estar vacío");
            
        var upperEvenOdd = evenOdd.ToUpper();
        if (!IsValid(upperEvenOdd))
            throw new ArgumentException($"Valor par/impar inválido: {evenOdd}. Valores válidos: {string.Join(", ", ValidEvenOdd)}");
            
        return upperEvenOdd;
    }
    
    public static string GetEvenOddForNumber(int number)
    {
        RouletteNumber.Validate(number);
        
        if (number == 0)
            throw new ArgumentException("El número 0 no es par ni impar");
            
        return number % 2 == 0 ? EVEN : ODD;
    }
}
