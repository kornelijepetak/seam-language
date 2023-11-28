using System.Text;
using static System.Char;
using static SeamCompiler.LexicalAnalysis.Token.Other;

namespace SeamCompiler.LexicalAnalysis.States;

class IntegerLiteralState(Position startingPosition) : IState
{
    private readonly StringBuilder lexeme = new();

    public IState? ProcessCharacter(char character, Position position, ILexicalAnalysisContext context)
    {
        if (IsAsciiDigit(character))
        {
            context.ConsumeCharacters(1);
            lexeme.Append(character);
            return this;
        }

        if (character == '.')
        {
            context.ConsumeCharacters(1);
            return new DecimalLiteralState(lexeme, startingPosition);
        }

        context.CreateToken(
            NumberConstant(lexeme),
            startingPosition,
            lexeme.Length);

        return null;
    }
}
