using CsvParser.Helpers;
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace CsvParserTest
{
    public class TokenizerTest
    {
        [Fact]
        public void TokenizeHeaderTest()
        {
            var test = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"";
            var expected = new List<string> { "Patient Name", "SSN", "Age", "Phone Number", "Status" };

            var tokenizer = new Tokenizer();

            var actual = tokenizer.Tokenize(test);

            actual.Should().HaveCount(expected.Count);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void TokenizeDataTest()
        {
            var test = "\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"";
            var expected = new List<string> { "Prescott, Zeke", "542-51-6641", "21", "801-555-2134", "Opratory=2,PCP=1" };

            var tokenizer = new Tokenizer();

            var actual = tokenizer.Tokenize(test);

            actual.Should().HaveCount(expected.Count);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
