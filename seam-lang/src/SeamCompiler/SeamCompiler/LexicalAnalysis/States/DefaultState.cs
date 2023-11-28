using static System.Char;
using static SeamCompiler.LexicalAnalysis.Token.Interpunction;
using static SeamCompiler.LexicalAnalysis.Token.Operators;

namespace SeamCompiler.LexicalAnalysis.States;

class DefaultState : IState
{
    public IState? ProcessCharacter(char character, Position position, ILexicalAnalysisContext context)
    {
        if (IsAsciiDigit(character))
            return new IntegerLiteralState(position);

        if (IsAsciiLetter(character))
            return new IdentifierOrKeywordState(position);

        var operatorToken = getOperatorToken(character, context);
        if (operatorToken.HasValue)
        {
            var token = operatorToken.Value;
            var length = token.Lexeme.Length;
            context.CreateToken(operatorToken.Value, position, length);
            context.ConsumeCharacters(length);
            return this;
        }

        consumeWhitespace(character, context);

        return this;
    }

    private static void consumeWhitespace(char character, ILexicalAnalysisContext context)
    {
        if (character == '\r' && context.NextCharacter == '\n')
        {
            context.ConsumeNewline(2);
            return;
        }

        if (character is '\r' or '\n')
        {
            context.ConsumeNewline(1);
            return;
        }

        if (IsWhiteSpace(character))
        {
            context.ConsumeCharacters(1);
            return;
        }
    }

    private static Token? getOperatorToken(char character, ILexicalAnalysisContext context)
        => character switch
        {
            '(' => OpeningParenthesis,
            ')' => ClosingParenthesis,
            '{' => OpeningBrace,
            '}' => ClosingBrace,
            ':' => Colon,
            ',' => Comma,
            '+' => Addition,
            '*' => Multiplication,
            '/' => Division,
            '!' when context.NextCharacter == '=' => NotEqual,
            '=' when context.NextCharacter == '=' => Equal,
            '=' => Assignment,
            '-' when context.NextCharacter == '>' => SingleArrow,
            '-' => Subtraction,
            '>' when context.NextCharacter == '=' => GreaterThanOrEqual,
            '>' => GreaterThan,
            '<' when context.NextCharacter == '=' => LessThanOrEqual,
            '<' => LessThan,
            _ => null
        };
}
