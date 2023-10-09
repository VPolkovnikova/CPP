using System.Numerics;


var input = File.ReadAllText("INPUT.TXT");
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


static void OutputWrite(string content) => File.WriteAllText("OUTPUT.TXT", content);