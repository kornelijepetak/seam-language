using SeamCompiler.LexicalAnalysis.States;

namespace SeamCompiler.LexicalAnalysis;

public class LexicalAnalyzer : ILexicalAnalysisContext
{
    private IState state;

    private int offset = 0;
    private int column = 1;
    private int line = 1;

    private readonly string code = "";

    private readonly List<TokenEnvelope> tokens = [];

    public static List<TokenEnvelope> Analyze(string code)
    {
        var analyzer = new LexicalAnalyzer(code);
        analyzer.analyze();
        return analyzer.tokens;
    }

    private LexicalAnalyzer(string code)
    {
        this.code = code;
        state = new DefaultState();
    }

    private void analyze()
    {
        while (offset < code.Length)
        {
            var position = new Position(line, column, offset);
            var character = code[offset];
            
            state = state.ProcessCharacter(character, position, this) 
                ?? new DefaultState();
        }
    }

    public void ConsumeCharacters(int count)
    {
        offset += count;
        column += count;
    }

    public void ConsumeNewline(int length)
    {
        line++;
        offset += length;
        column = 1;
    }

    public void CreateToken(Token token, Position position, int length)
    {
        var envelope = new Envelope(position, length);
        var tokenEnvelope = new TokenEnvelope(token, envelope);
        tokens.Add(tokenEnvelope);
    }

    public char? NextCharacter
        => offset + 1 < code.Length
        ? code[offset + 1]
        : null;
}
