using PostfixInterpreter;
using static System.Console;
using static System.ConsoleColor;

Write("Expression (leave empty for demo): ");

var input = ReadLine();

var expression = string.IsNullOrEmpty(input)
    ? "3 4 * 8 + 16 2 7 * - *"
    : input;

WriteLine($"Expression: {expression}");

try
{
    var result = Interpreter.Evaluate(expression);

    WriteLine($"Result: {result}");
}
catch (Exception ex)
{
    var savedColor = ForegroundColor;
    ForegroundColor = Red;
    WriteLine($"ERROR: {ex.Message}"); 
    ForegroundColor = savedColor;
}
