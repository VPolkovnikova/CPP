namespace Logics;


public static class LaboratoryWork2
{
    public static void Run(string inputPath, string outputPath)
    {
        var inputLines = File.ReadAllLines(inputPath).AsEnumerable().GetEnumerator();

        if (!inputLines.MoveNext())
        {
            OutputWrite("The input does not contain the number of steps.");
            return;
        }
        if (!(
            int.TryParse(inputLines.Current, out var numberNumbers)
            && numberNumbers >= 0
        ))
        {
            OutputWrite("Invalid number of steps.");
            return;
        }

        if (!inputLines.MoveNext())
        {
            OutputWrite("The input does not contain numbers written on the steps.");
            return;
        }
        var numberTexts = inputLines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (numberTexts.Length != numberNumbers)
        {
            OutputWrite($"Expected {numberNumbers} numbers, but received {numberTexts.Length}.");
            return;
        }

        var numbers = new int[numberNumbers];
        for (int i = 0; i < numberNumbers; ++i)
        {
            if (!int.TryParse(numberTexts[i], out numbers[i]))
            {
                OutputWrite($"Invalid number №{i + 1}.");
                return;
            }
        }

        var path = Calculate(numbers);
        var sum = path
            .Select(index => numbers[index])
            .Sum();
        var pathText = string.Join(
            ' ',
            path.Select(index => index + 1)
        );
        OutputWrite($"""
            {sum}
            {pathText}
            """);


        void OutputWrite(string content) => File.WriteAllText(outputPath, content);
    }


    private static LinkedList<int> Calculate(int[] numbers)
    {
        var path = new LinkedList<int>();
        if (numbers.Length == 0)
        {
            return path;
        }
        if (numbers.Length == 1)
        {
            path.AddFirst(0);
            return path;
        }

        var jumps = new int[numbers.Length];
        var maxSums = new int?[numbers.Length];

        const int StartIndex = -1;
        jumps[FillSteps(0) > FillSteps(1) ? 0 : 1] = StartIndex;
        for (var i = numbers.Length - 1; i != StartIndex; i = jumps[i])
        {
            path.AddFirst(i);
        }
        return path;


        int FillSteps(int n)
        {
            ref var result = ref maxSums[n];
            if (result.HasValue)
            {
                return result.Value;
            }

            var firstIndex = n + 1;
            if (firstIndex == maxSums.Length)
            {
                result = numbers[n];
                return result.Value;
            }
            var firstMaxSum = FillSteps(firstIndex);

            var secondIndex = n + 2;
            if (secondIndex == maxSums.Length)
            {
                jumps[firstIndex] = n;
                result = numbers[n] + firstMaxSum;
                return result.Value;
            }
            var secondMaxSum = FillSteps(secondIndex);

            if (firstMaxSum > secondMaxSum)
            {
                jumps[firstIndex] = n;
                result = numbers[n] + firstMaxSum;
                return result.Value;
            }
            else
            {
                jumps[secondIndex] = n;
                result = numbers[n] + secondMaxSum;
                return result.Value;
            }
        }
    }
}