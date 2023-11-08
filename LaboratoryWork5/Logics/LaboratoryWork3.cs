namespace Logics;


public static class LaboratoryWork3
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
            return "Вхідні дані не містять кількості людей та номер людини.";
        }
        var valueTexts = inputLines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (!(
            valueTexts is [var sizeText, var numberText]
            && int.TryParse(sizeText, out var size)
            && size > 0
            && int.TryParse(numberText, out var number)
            && number > 0
            && number <= size
        ))
        {
            return "Недійсна кількість людей чи номер людини.";
        }

        var matrix = new bool[size, size];
        for (var i = 0; i < size; ++i)
        {
            if (!inputLines.MoveNext())
            {
                return GetInvalidRowMessage(i, "Не міститься у вхідних даних.");
            }
            valueTexts = inputLines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (valueTexts.Length != size)
            {
                return GetInvalidRowMessage(
                    i,
                    $"Очікувалась кількість значень: {size}. Отримана кількість значень: {valueTexts.Length}."
                );
            }

            for (var j = 0; j < size; ++j)
            {
                const string
                    Uncertainty = "0",
                    Friendship = "1";

                var valueText = valueTexts[j];
                ref var value = ref matrix[i, j];
                if (valueText == Uncertainty)
                {
                    value = false;
                }
                else if (valueText == Friendship)
                {
                    value = true;
                }
                else
                {
                    return GetInvalidValueMessage(i, j, "Недійсне значення.");
                }

                if (value && i == j)
                {
                    return GetInvalidValueMessage(
                        i,
                        j,
                        $"Очікувалось '{Uncertainty}'. Людина не може бути собі другом."
                    );
                }
                if (j < i && value != matrix[j, i])
                {
                    return GetInvalidValueMessage(
                        i,
                        j,
                        "Дружба - двосторонній зв'язок, тому значення мають бути" +
                        " симетричні відносно головної діагоналі."
                    );
                }
            }
        }

        var index = number - 1;
        return CalculateNumberFriends(matrix, index).ToString();
    }


    private static string GetInvalidRowMessage(int i, string message) => $"Рядок {i + 1}. {message}";


    private static string GetInvalidValueMessage(int i, int j, string message)
    {
        return $"Рядок {i + 1}, стовпчик {j + 1}. {message}";
    }


    private static int CalculateNumberFriends(bool[,] matrix, int index)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            throw new ArgumentException("Має бути квадратною.", nameof(matrix));
        }
        var size = matrix.GetLength(0);

        var isVisited = new bool[size];
        return Visit(index);


        int Visit(int i)
        {
            isVisited[i] = true;
            var numberFriends = 0;
            for (int j = 0; j < size; ++j)
            {
                if (matrix[i, j] && !isVisited[j])
                {
                    numberFriends = 1 + Visit(j);
                }
            }
            return numberFriends;
        }
    }
}