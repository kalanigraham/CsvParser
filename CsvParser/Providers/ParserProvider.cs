using CsvParser.Helpers.Interfaces;
using CsvParser.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser.Providers
{
    public class ParserProvider : IParserProvider
    {
        private NewLineConfig config = 0;
        private ITokenizer _tokenizer;

        public ParserProvider(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public string ParseCsv(string data)
        {
            config = DetermineNewLineConfiguration(data);
            var lines = SplitLines(data);
            var sb = new StringBuilder();
            foreach (var line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    var tokens = _tokenizer.Tokenize(line);
                    sb.Append(MergeLine(tokens) + GetNewLine());
                }
            }
            return sb.ToString();
        }

        private NewLineConfig DetermineNewLineConfiguration(string data)
        {
            if (data.Contains("\r\n")) return NewLineConfig.rn;
            if (data.Contains("\n")) return NewLineConfig.n;
            throw new ArgumentException("Invalid new line configuration");
        }

        private IEnumerable<string> SplitLines(string data)
        {
            if (config == NewLineConfig.n)
            {
                return data.Split("\n");
            }
            else if (config == NewLineConfig.rn)
            {
                return data.Split("\r\n");
            }
            else
            {
                throw new ArgumentException("New line configuration is not set");
            }
        }

        private string MergeLine(IEnumerable<string> tokens)
        {
            var sb = new StringBuilder();
            foreach (var token in tokens)
            {
                sb.Append("[" + token + "] ");
            }
            return sb.ToString().Trim();
        }

        private string GetNewLine()
        {
            if (config == NewLineConfig.n) return "\n";
            if (config == NewLineConfig.rn) return "\r\n";
            throw new ArgumentException("New line configuration is not set");
        }

    }

    public enum NewLineConfig
    {
        NotSet = 0,
        n = 1,
        rn = 2
    }
}
