using CsvParser.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvParser.Helpers
{
    public class TokenList : ITokenList
    {
        public char? CurrentToken { get; set; }

        public List<char> Tokens { get; set; }

        public TokenList(List<char> tokens)
        {
            Tokens = tokens;
            CurrentToken = null;
        }

        public bool IsToken(char ch)
        {
            if (Tokens.Contains(ch))
            {
                CurrentToken = ch;
                return true;
            }
            return false;
        }
    }
}
