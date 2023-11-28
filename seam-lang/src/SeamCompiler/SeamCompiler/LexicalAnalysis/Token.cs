using System.Text;
using SeamCompiler.Utils;

namespace SeamCompiler.LexicalAnalysis;

public readonly record struct Token(string Name, string Lexeme)
{
    public static class Keywords
    {
        public static Token Var { get; } = new("VarKeyword", "var");
        public static Token Function { get; } = new("FunctionKeyword", "function");
        public static Token If { get; } = new("IfKeyword", "if");
        public static Token Else { get; } = new("ElseKeyword", "else");
        public static Token While { get; } = new("WhileKeyword", "while");
        public static Token Return { get; } = new("ReturnKeyword", "return");
        public static Token Break { get; } = new("BreakKeyword", "break");
        public static Token Continue { get; } = new("ContinueKeyword", "continue");

        public static Token? GetTokenBasedOnLexeme(Either<string, StringBuilder> lexeme)
        {
            return lexeme.CollapseToString() switch
            {
                "var" => Var,
                "function" => Function,
                "if" => If,
                "else" => Else,
                "while" => While,
                "return" => Return,
                "break" => Break,
                "continue" => Continue,
                _ => null
            };
        }
    }

    public static class Operators
    {
        public static Token Assignment { get; } = new("AssignmentOperator", "=");
        public static Token Addition { get; } = new("AdditionOperator", "+");
        public static Token Subtraction { get; } = new("SubtractionOperator", "-");
        public static Token Division { get; } = new("DivisionOperator", "/");
        public static Token Multiplication { get; } = new("MultiplicationOperator", "*");
        public static Token GreaterThan { get; } = new("GreaterThanOperator", ">");
        public static Token GreaterThanOrEqual { get; } = new("GreaterThanOrEqualOperator", ">=");
        public static Token LessThan { get; } = new("LessThanOperator", "<");
        public static Token LessThanOrEqual { get; } = new("LessThanOrEqualOperator", "<=");
        public static Token NotEqual { get; } = new("NotEqualOperator", "!=");
        public static new Token Equal { get; } = new("EqualsOperator", "==");
    }

    public static class Interpunction
    {
        public static Token OpeningParenthesis { get; } = new("OpeningParenthesis", "(");
        public static Token ClosingParenthesis { get; } = new("ClosingParenthesis", ")");
        public static Token OpeningBrace { get; } = new("OpeningBrace", "{");
        public static Token ClosingBrace { get; } = new("ClosingBrace", "}");

        public static Token Colon { get; } = new("Colon", ":");
        public static Token Comma { get; } = new("Comma", ",");
        public static Token SingleArrow { get; } = new("SingleArrow", "->");
    }

    public static class Other
    {
        public static Token Identifier(Either<string, StringBuilder> lexeme)
            => new("Identifier", lexeme.CollapseToString());

        public static Token NumberConstant(Either<string, StringBuilder> lexeme)
            => new("NumberConstant", lexeme.CollapseToString());
    }
}

