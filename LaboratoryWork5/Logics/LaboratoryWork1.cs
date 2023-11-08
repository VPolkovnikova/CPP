using System.Numerics;

namespace Logics;


public static class LaboratoryWork1
{
    public static string Run(string input)
    {
        if (!(
            int.TryParse(input, out var numberCenters)
            && numberCenters > 0
        ))
        {
            return "Вхідний файл має містити натуральне число.";
        }
        var result = BigInteger.Pow(3, numberCenters * (numberCenters - 1) / 2);
        return result.ToString();
    }
}