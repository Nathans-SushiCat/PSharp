using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PSharp
{
    public enum Keywords{
        Wenn,
        Ist,
        Nicht,
        Funktion,
        Rufe,
        Variable,
        Dann,
        Kommentar,
        Ausgabe,
        Eingabe,
        Ende,
        Zahl,
        Wort,
        Und,
        Oder,
        Punkt,
        Eigenname
        
    }

    public class SyntaxKind 
    {
        public object data;
        public Keywords keyword;

        public SyntaxKind(object data, Keywords keyword)
        {
            this.data = data;
            this.keyword = keyword;
        }
    }


    internal class Lexer
    {
        string code;
        int currentposition = 0;

        Dictionary<string, Keywords> syntax = new Dictionary<string, Keywords>
        {
            { "wenn", Keywords.Wenn },
            { "ist", Keywords.Ist},
            { "nicht", Keywords.Nicht},
            { "funktion", Keywords.Funktion},
            { "rufe", Keywords.Rufe},
            { "variable", Keywords.Variable},
            { "dann", Keywords.Dann},
            { "kommentar", Keywords.Kommentar},
            { "ausgabe", Keywords.Ausgabe},
            { "eingabe", Keywords.Eingabe},
            { ".", Keywords.Ende},
            { "und", Keywords.Und},
            { "oder", Keywords.Oder},
            { ",", Keywords.Punkt},
        };



    public Lexer(string code)
        {
            this.code = code;

        }

        public SyntaxKind Lex()

        {
            if (currentposition >= code.Length)
                return null;

            while (code[currentposition] == '\n' || code[currentposition] == '\r' || code[currentposition] == ' ')
            {
                Advance();
            }

            if (char.IsDigit(code[currentposition]))
            {
                return GetNumberString();
            }

            foreach (var item in syntax)
            {
                if (IsKeyword(item.Key))
                {
                    return new SyntaxKind(null, item.Value);
                }
            }

            if (char.IsLetter(code[currentposition]))
                return new SyntaxKind(GetEigennamen(), Keywords.Eigenname);

            return null;
        }

        private string GetEigennamen()
        {
            StringBuilder result = new StringBuilder();
            while (code[currentposition] != '\0' && (char.IsLetterOrDigit(code[currentposition]) || code[currentposition] == '_'))
            {
                result.Append(code[currentposition]);
                Advance();
            }
            return result.ToString();
        }


        private SyntaxKind GetNumberString()
        {
            StringBuilder result = new StringBuilder();

            if (currentposition >= code.Length)
                return null;
            while (code[currentposition] != '\0' && char.IsDigit(code[currentposition]) || code[currentposition] == ',')
            {
                result.Append(code[currentposition]);
                Advance();
                if (currentposition >= code.Length)
                    break;
            }
            return new SyntaxKind(result.ToString(), Keywords.Zahl);
        }

        public void Advance(int val = 1)
        {
            currentposition += val;
        }

        public bool IsKeyword(string key)
        {
            if (code[currentposition] != key[0])
                return false;

            if (key.Length + currentposition >= code.Length)
                return false;

            if (code.Substring(currentposition, key.Length).Equals(key))
            {
                Advance(key.Length);
                return true;
            }

            return false;
        }
    }
}
