using System.Text;
using static System.Char;
using static SeamCompiler.LexicalAnalysis.Token.Other;

namespace SeamCompiler.LexicalAnalysis.States;

class DecimalLiteralExponentState : IState
{
    private readonly StringBuilder lexeme = new();
    private readonly Position startingPosition;

    bool signWasRead = true;
    bool digitWasRead = true;

    public DecimalLiteralExponentState(StringBuilder decimalPart, Position startingPosition)
    {
        this.startingPosition = startingPosition;
        lexeme.Append(decimalPart);
        lexeme.Append('E');
    }

    public IState? ProcessCharacter(char character, Position position, ILexicalAnalysisContext context)
    {
        if (!signWasRead && IsAsciiDigit(character))
        {
            digitWasRead = true;
            context.ConsumeCharacters(1);
            lexeme.Append(character);
            return this;
        }

        if (!signWasRead && !digitWasRead && character is '+' or '-')
        {
            signWasRead = true;
            context.ConsumeCharacters(1);
            lexeme.Append(character);
            return this;
        }

        context.CreateToken(
            NumberConstant(lexeme),
            startingPosition,
            lexeme.Length);

        return null;
    }
}