using System.Collections.Frozen;
using System.Text;
using CommandLine;
using CommandLine.Text;
using Logics;


Console.InputEncoding = Console.OutputEncoding = Encoding.UTF8;

var result = new Parser().ParseArguments<RunOptions, SetPathOptions>(args);
var message = result
    .MapResult<RunOptions, SetPathOptions, string>(
        OptionsMapper.MapRunOptions,
        OptionsMapper.MapSetPathOptions,
        errors => OptionsMapper.MapErrors(result, errors)
    );
Console.WriteLine(message);


file static class OptionsMapper
{
    private static readonly Version _version;
    private static readonly FrozenDictionary<string, Action<string, string>> _runMethods;


    static OptionsMapper()
    {
        _version = typeof(OptionsMapper)
            .Assembly
            .GetName()
            .Version
            ?? throw new InvalidOperationException("Збірка не містить інформації про версію.");
        _runMethods = new Dictionary<string, Action<string, string>>
        {
            ["lab1"] = LaboratoryWork1.Run,
            ["lab2"] = LaboratoryWork2.Run,
            ["lab3"] = LaboratoryWork3.Run
        }
        .ToFrozenDictionary();
    }


    public static string MapErrors<T>(ParserResult<T> result, IEnumerable<Error> errors)
    {
        return errors.IsVersion()
            ? $"""
            Author: Полковнікова Владислава Сергіївна
            Version: {_version}
            """
            : HelpText.AutoBuild(result);
    }


    public static string MapRunOptions(RunOptions options)
    {
        if (!_runMethods.TryGetValue(options.LaboratoryWorkName, out var run))
        {
            return GetErrorMessage("Недійсна назва лабораторної роботи.");
        }

        var directoryPath =
            Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User)
            ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var inputPath = options.InputPath ?? Path.Join(directoryPath, "INPUT.TXT");
        var outputPath = options.OutputPath ?? Path.Join(directoryPath, "OUTPUT.TXT");
        try
        {
            run(inputPath, outputPath);
        }
        catch (IOException exception)
        {
            return $"Помилка введення чи виведення. {exception.Message}";
        }
        return "Виконання завершено!";
    }


    public static string MapSetPathOptions(SetPathOptions options)
    {
        Environment.SetEnvironmentVariable(
            "LAB_PATH",
            options.FilesDirectoryPath,
            EnvironmentVariableTarget.User
        );
        return "Змінну середовища встановлено!";
    }


    private static string GetErrorMessage(string message) => $"Помилка. {message}";
}


[Verb("run", HelpText = "Run laboratory work.")]
file class RunOptions
{
    [Value(0, HelpText = "Laboratory work name ('lab1', 'lab2' or 'lab3').", Required = true)]
    public required string LaboratoryWorkName { get; init; }

    [Option('i', "input", HelpText = "The path to the input file.")]
    public string? InputPath { get; init; }

    [Option('o', "output", HelpText = "The path to the output file.")]
    public string? OutputPath { get; init; }
}


[Verb(
    "set-path",
    HelpText = "Set the path to the directory with the input/output files as an environment variable."
)]
file class SetPathOptions
{
    [Option('p', "path", HelpText = "The path to the directory with the input/output files.", Required = true)]
    public required string FilesDirectoryPath { get; init; }
}