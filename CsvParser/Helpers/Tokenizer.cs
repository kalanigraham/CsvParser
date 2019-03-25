using CsvParser.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvParser.Helpers
{
    public class Tokenizer : ITokenizer
    {
        public TokenList TokenSymbols { get; set; }

        public TokenList FieldDelimiters { get; set; }

        public Tokenizer()
        {
            TokenSymbols = new TokenList(new List<char> { ',' });
            FieldDelimiters = new TokenList(new List<char> { '\"' });
        }

        public Tokenizer(List<char> tokens, List<char> fieldDelimiters)
        {
            TokenSymbols = new TokenList(tokens);
            FieldDelimiters = new TokenList(fieldDelimiters);
        }


        public IEnumerable<string> Tokenize(string data)
        {
            var list = new List<string>();
            var inField = false;
            int fieldStart = -1;
            int fieldEnd = -1;
            for(int i=0;i<data.Length;i++)
            {
                if (FieldDelimiters.IsToken(data[i]))
                {
                    //Start a new field
                    if (!inField)
                    {
                        inField = true;
                        fieldStart = i+1;
                        //Advance to end of field
                        i = data.IndexOf((char)FieldDelimiters.CurrentToken, i + 1) - 1;
                        continue;
                    }
                    else
                    {
                        //End the field
                        inField = false;
                        fieldEnd = i;
                    }
                }
                else if (TokenSymbols.IsToken(data[i]))
                {
                    if (fieldEnd == -1)
                    {
                        fieldEnd = i;
                    }
                    list.Add(data.Substring(fieldStart, fieldEnd - fieldStart));
                    fieldStart = i+1;
                    fieldEnd = -1;
                }
            }
            list.Add(data.Substring(fieldStart, fieldEnd - fieldStart));
            return list;
        }
    }
}
