namespace PostfixInterpreter;

public static class Interpreter
{
    public static double Evaluate(string expression)
    {
        var tokens = expression
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        var stack = new Stack<double>();

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out var number))
            {
                stack.Push(number);
                continue;
            }

            if (stack.Count < 2)
                throw new InvalidOperationException("Not enough operands for the operation.");

            stack.Push(calculate(token, stack));
        }

        if (stack.Count != 1)
            throw new InvalidOperationException("Too many numbers in the expression");

        return stack.Pop();
    }

    private static double calculate(string token, Stack<double> stack)
    {
        var second = stack.Pop();
        var first = stack.Pop();

        return token switch
        {
            "+" => first + second,
            "-" => first - second,
            "*" => first * second,
            "/" => first / second,
            _ => throw new InvalidOperationException($"Unknown operator {token}")
        };
    }
}
