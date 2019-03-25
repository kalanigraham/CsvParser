using CsvParser.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CsvParser.Providers;
using FluentAssertions;
using CsvParser.Helpers;

namespace CsvParserTest
{
    public class ParserProviderTest
    {
        [Fact]
        public void ParseCsvHeaderTest()
        {
            var test = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\r\n";
            var moqResult = new List<string> { "Patient Name", "SSN", "Age", "Phone Number", "Status" };
            var expected = "[Patient Name] [SSN] [Age] [Phone Number] [Status]\r\n";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(moqResult);

            var parser = new ParserProvider(tokenizer.Object);

            var actual = parser.ParseCsv(test);

            actual.Should().BeEquivalentTo(expected);
            tokenizer.Verify(m => m.Tokenize(It.IsAny<string>()));
        }

        [Fact]
        public void ParseCsvDataTest()
        {
            var test = "\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"\n";
            var moqResult = new List<string> { "Prescott, Zeke", "542-51-6641", "21", "801-555-2134", "Opratory=2,PCP=1" };
            var expected = "[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]\n";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(moqResult);

            var parser = new ParserProvider(tokenizer.Object);

            var actual = parser.ParseCsv(test);

            actual.Should().BeEquivalentTo(expected);
            tokenizer.Verify(m => m.Tokenize(It.IsAny<string>()));
        }

        [Fact]
        public void ParseCsvNTest()
        {
            var test = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\n\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"\n\"Goldstein, Bucky\",\"635-45-1254\",42,\"435-555-1541\",\"Opratory=1,PCP=1\"\n\"Vox, Bono\",\"414-45-1475\",51,\"801-555-2100\",\"Opratory=3,PCP=2\"\n";
            var expected = "[Patient Name] [SSN] [Age] [Phone Number] [Status]\n[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]\n[Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1]\n[Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]\n";

            var parser = new ParserProvider(new Tokenizer());
            var actual = parser.ParseCsv(test);

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void ParseCsvRNTest()
        {
            var test = "\"Patient Name\",\"SSN\",\"Age\",\"Phone Number\",\"Status\"\r\n\"Prescott, Zeke\",\"542-51-6641\",21,\"801-555-2134\",\"Opratory=2,PCP=1\"\r\n\"Goldstein, Bucky\",\"635-45-1254\",42,\"435-555-1541\",\"Opratory=1,PCP=1\"\r\n\"Vox, Bono\",\"414-45-1475\",51,\"801-555-2100\",\"Opratory=3,PCP=2\"\r\n";
            var expected = "[Patient Name] [SSN] [Age] [Phone Number] [Status]\r\n[Prescott, Zeke] [542-51-6641] [21] [801-555-2134] [Opratory=2,PCP=1]\r\n[Goldstein, Bucky] [635-45-1254] [42] [435-555-1541] [Opratory=1,PCP=1]\r\n[Vox, Bono] [414-45-1475] [51] [801-555-2100] [Opratory=3,PCP=2]\r\n";

            var parser = new ParserProvider(new Tokenizer());
            var actual = parser.ParseCsv(test);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
