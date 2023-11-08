namespace Logics;


public static class LaboratoryWork2
{
    public static string Run(string input)
    {
        var inputLines = input
            .ReplaceLineEndings()
            .Split(Environment.NewLine)
            .AsEnumerable()
            .GetEnumerator();

        if (!inputLines.MoveNext())
        {
            return "Вхідний файл не містить кількості сходинок.";
        }
        if (!(
            int.TryParse(inputLines.Current, out var numberNumbers)
            && numberNumbers >= 0
        ))
        {
            return "Недійсна кількість сходинок.";
        }

        if (!inputLines.MoveNext())
        {
            return "Вхідний файл не містить чисел, написаних на сходинках.";
        }
        var numberTexts = inputLines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (numberTexts.Length != numberNumbers)
        {
            return
                $"Очікувалась кількість чисел: {numberNumbers}." +
                $" Отримана кількість чисел: {numberTexts.Length}.";
        }

        var numbers = new int[numberNumbers];
        for (int i = 0; i < numberNumbers; ++i)
        {
            if (!int.TryParse(numberTexts[i], out numbers[i]))
            {
                return $"Недійсне значення №{i + 1}.";
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
        return $"""
            {sum}
            {pathText}
            """;
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