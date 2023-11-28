using FluentAssertions;
using SeamCompiler.LexicalAnalysis;
using SeamCompiler.Tests.Utilities;
using Xunit;
using static SeamCompiler.LexicalAnalysis.Token.Interpunction;
using static SeamCompiler.LexicalAnalysis.Token.Keywords;
using static SeamCompiler.LexicalAnalysis.Token.Other;
using static SeamCompiler.LexicalAnalysis.Token.Operators;

namespace SeamCompiler.Tests.LexicalAnalysis;

public class LexicalAnalysisTests
{
    [Fact]
    public void HasCorrectTokensForSimpleProgram()
    {
        var expectedTokens = new[]
        {
            Function,
            Identifier("identity"),
            OpeningParenthesis,
            Identifier("x"),
            Colon,
            Identifier("number"),
            ClosingParenthesis,
            SingleArrow,
            Identifier("number"),
            OpeningBrace,
            Return,
            Identifier("x"),
            ClosingBrace
        };

        var tokens = analyze("function.seam");

        tokens.Select(t => t.Token).Should().Equal(expectedTokens);
    }

    [Fact]
    public void HasCorrectTokensForLargerProgram()
    {
        var expectedTokens = new[]
        {
            Function,
            Identifier("calc"),
            OpeningParenthesis,
            Identifier("x"),
            Colon,
            Identifier("number"),
            ClosingParenthesis,
            SingleArrow,
            Identifier("number"),
            OpeningBrace,
            Var,
            Identifier("i"),
            Assignment,
            NumberConstant("10"),
            Var,
            Identifier("count"),
            Assignment,
            NumberConstant("0"),
            While,
            OpeningParenthesis,
            Identifier("i"),
            GreaterThan,
            NumberConstant("0"),
            ClosingParenthesis,
            OpeningBrace,
            If,
            OpeningParenthesis,
            Identifier("count"),
            Equal,
            Identifier("x"),
            ClosingParenthesis,
            OpeningBrace,
            Continue,
            ClosingBrace,
            Identifier("count"),
            Assignment,
            Identifier("count"),
            Addition,
            NumberConstant("1"),
            Identifier("i"),
            Assignment,
            Identifier("i"),
            Subtraction,
            NumberConstant("1"),
            If,
            OpeningParenthesis,
            Identifier("count"),
            GreaterThan,
            Identifier("x"),
            ClosingParenthesis,
            OpeningBrace,
            Break,
            ClosingBrace,
            ClosingBrace,
            Return,
            Identifier("count"),
            ClosingBrace
        };

        var tokens = analyze("while-loop.seam");

        tokens.Select(t => t.Token).Should().Equal(expectedTokens);
    }



    private static List<TokenEnvelope> analyze(string file)
    {
        var text = EmbeddedResources.LexicalAnalysis.Get(file);
        return LexicalAnalyzer.Analyze(text);
    }
}
