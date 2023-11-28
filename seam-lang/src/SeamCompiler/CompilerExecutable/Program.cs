using SeamCompiler.LexicalAnalysis;

if (args.Length == 0)
{
    Console.WriteLine("You need to provide a file to compile.");
    Console.WriteLine("Example: > seam myCode.seam");
    return;
}

var code = File.ReadAllText(args[0]);

var tokens = LexicalAnalyzer.Analyze(code);

foreach (var token in tokens)
    Console.WriteLine(token);

