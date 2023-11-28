namespace SeamCompiler.LexicalAnalysis;

internal interface ILexicalAnalysisContext
{
    char? NextCharacter { get; }
    void ConsumeCharacters(int count);
    void ConsumeNewline(int length);
    void CreateToken(Token token, Position position, int length);
}
