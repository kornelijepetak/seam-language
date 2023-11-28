using System.Text;
using static System.Char;
using static SeamCompiler.LexicalAnalysis.Token.Other;

namespace SeamCompiler.LexicalAnalysis.States;

class DecimalLiteralState : IState
{
    private readonly StringBuilder lexeme = new();
    private bool digitWasRead = false;
    private readonly Position startingPosition;

    public DecimalLiteralState(StringBuilder integerPart, Position startingPosition)
    {
        lexeme.Append(integerPart);
        lexeme.Append('.');

        this.startingPosition = startingPosition;
    }

    public IState? ProcessCharacter(char character, Position position, ILexicalAnalysisContext context)
    {
        if (IsAsciiDigit(character))
        {
            digitWasRead = true;
            context.ConsumeCharacters(1);
            lexeme.Append(character);
            return this;
        }

        if (digitWasRead && character is 'E' or 'e')
        {
            context.ConsumeCharacters(1);
            return new DecimalLiteralExponentState(lexeme, startingPosition);
        }

        context.CreateToken(
            NumberConstant(lexeme),
            startingPosition,
            lexeme.Length);

        return null;
    }
}
