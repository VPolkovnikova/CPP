using System.Numerics;

namespace Logics;


public static class LaboratoryWork1
{
    public static void Run(string inputPath, string outputPath)
    {
        var input = File.ReadAllText(inputPath);
        if (!(
            int.TryParse(input, out var numberCenters)
            && numberCenters > 0
        ))
        {
            OutputWrite("Вхідний файл має містити натуральне число.");
            return;
        }
        var result = BigInteger.Pow(3, numberCenters * (numberCenters - 1) / 2);
        OutputWrite(result.ToString());


        void OutputWrite(string content) => File.WriteAllText(outputPath, content);
    }
}