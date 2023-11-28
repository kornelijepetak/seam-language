using System.Text;
using static System.Char;
using static SeamCompiler.LexicalAnalysis.Token.Other;
using static SeamCompiler.LexicalAnalysis.Token.Keywords;

namespace SeamCompiler.LexicalAnalysis.States;

class IdentifierOrKeywordState(Position startingPosition) : IState
{
    private readonly StringBuilder lexeme = new();

    public IState? ProcessCharacter(char character, Position position, ILexicalAnalysisContext context)
    {
        if (IsAsciiLetterOrDigit(character) || character == '_')
        {
            context.ConsumeCharacters(1);
            lexeme.Append(character);
            return this;
        }

        if (GetTokenBasedOnLexeme(lexeme) is Token token)
        {
            context.CreateToken(
                token,
                startingPosition,
                token.Lexeme.Length);
        }
        else
        {
            context.CreateToken(
                Identifier(lexeme),
                startingPosition,
                lexeme.Length);
        }

        return null;
    }
}
