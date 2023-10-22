using Logics;


var inputLines = File.ReadAllLines("INPUT.TXT").AsEnumerable().GetEnumerator();

if (!inputLines.MoveNext())
{
    OutputWrite("Вхідні дані не містять кількості людей та номер людини.");
    return;
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
    OutputWrite("Недійсна кількість людей чи номер людини.");
    return;
}

var matrix = new bool[size, size];
for (var i = 0; i < size; ++i)
{
    if (!inputLines.MoveNext())
    {
        OutputWriteInvalidRow(i, "Не міститься у вхідних даних.");
        return;
    }
    valueTexts = inputLines.Current.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (valueTexts.Length != size)
    {
        OutputWriteInvalidRow(
            i,
            $"Очікувалась кількість значень: {size}. Отримана кількість значень: {valueTexts.Length}."
        );
        return;
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
            OutputWriteInvalidValue(i, j, "Недійсне значення.");
            return;
        }

        if (value && i == j)
        {
            OutputWriteInvalidValue(i, j, $"Очікувалось '{Uncertainty}'. Людина не може бути собі другом.");
            return;
        }
        if (j < i && value != matrix[j, i])
        {
            OutputWriteInvalidValue(
                i,
                j,
                "Дружба - двосторонній зв'язок, тому значення мають бути" +
                " симетричні відносно головної діагоналі."
            );
            return;
        }
    }
}

var index = number - 1;
OutputWrite(
    Algorithm
        .CalculateNumberFriends(matrix, index)
        .ToString()
);


static void OutputWrite(string content) => File.WriteAllText("OUTPUT.TXT", content);


static void OutputWriteInvalidRow(int i, string message) => OutputWrite($"Рядок {i + 1}. {message}");


static void OutputWriteInvalidValue(int i, int j, string message)
{
    OutputWrite($"Рядок {i + 1}, стовпчик {j + 1}. {message}");
}