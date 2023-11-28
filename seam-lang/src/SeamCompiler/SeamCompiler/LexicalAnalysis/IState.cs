namespace SeamCompiler.LexicalAnalysis;

internal interface IState
{
    IState? ProcessCharacter(
        char character,
        Position position,
        ILexicalAnalysisContext context);
}
