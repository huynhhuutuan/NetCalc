// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Linq;

//  dotnet run "calc 360/(6-10)"
// if (args.Length != 1 || !args[0].StartsWith("calc "))
// {
//     Console.WriteLine("Usage: calc <expression>");
//     return;
// }

// dotnet run "360/(6-10)"
if (args.Length != 1)
{
    Console.WriteLine("Usage: <expression>");
    return;
}

// Get the path to the current user's home directory
var userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
// Combine the user directory with the file name "abc"
var historyFilePath = Path.Combine(userDirectory, ".calc_history");

var expression = args[0];
if(expression.StartsWith("history"))
{
    int.TryParse(expression.AsSpan("history".Length), out var lineCount);
    if (!File.Exists(historyFilePath))
        return;
    // Read all lines and get the last 30 lines
    var allLines = await File.ReadAllLinesAsync(historyFilePath);
    if (lineCount < 1)
        lineCount = 30;
    if (lineCount > allLines.Length)
        lineCount = allLines.Length;
    var lastLines = allLines.TakeLast(lineCount);
    Console.WriteLine($"Take last {lineCount} of total {allLines.Length}");
    Console.WriteLine(string.Join(Environment.NewLine, lastLines));
    return;
}

if(expression.StartsWith("calc "))
    expression = expression.Substring("calc ".Length); // Remove "calc " prefix

try
{
    var result = EvaluateExpression(expression);
    Console.WriteLine($"{expression} = {result}");
    await File.AppendAllTextAsync(historyFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fffffzzz}] {expression} = {result}{Environment.NewLine}");
}
catch (IOException ex)
{
    // Console.WriteLine(ex);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

return;

double EvaluateExpression(string exp)
{
    var e = new NCalc.Expression(exp);
    if (e.HasErrors())
    {
        throw new ArgumentException("Invalid expression format.");
    }
    return Convert.ToDouble(e.Evaluate());
}
