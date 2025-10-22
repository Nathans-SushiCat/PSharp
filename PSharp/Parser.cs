using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSharp
{
    
    internal class Parser
    {
        public Lexer lexer;
        public AbstractSyntaxTree node;
        public AbstractSyntaxTree root;
        public SyntaxKind currentToken;
        public SyntaxKind lastToken;

        public Parser(Lexer lexer)
        {
            root = new AbstractSyntaxTree();
            this.lexer = lexer;
            node = root;
            currentToken = lexer.Lex();
        }

        public AbstractSyntaxTree Parse()
        {
           
            while(node != null)
            {
                node = node.NextToken = Identify();
            }
            return root;
        }

        public AbstractSyntaxTree Identify(SyntaxKind token = null)
        {
            if (token == null)
                token = currentToken;
            if(token == null)
                return null;
            
            switch (token.keyword)
            {
                case Keywords.Variable:
                    NextToken(Keywords.Variable);
                    var variablename = currentToken.data;
                    NextToken(Keywords.Eigenname);
                    NextToken(Keywords.Ist);
                    var value = currentToken.data;
                    NextToken(Keywords.Wort, Keywords.Zahl);
                    NextToken(Keywords.Ende);
                    return new AST_Variable(value, variablename.ToString());
               case Keywords.Ausgabe:
                    NextToken(Keywords.Ausgabe);
                    List<AbstractSyntaxTree> val = new List<AbstractSyntaxTree>();
                    do
                    {
                         val.Add(Identify());
                    }
                    while(currentToken != null && currentToken.keyword == Keywords.Ende );
                  
                    return new AST_Ausgabe(val, "");
                case Keywords.Zahl:
                    var varValue = currentToken.data;
                    NextToken(Keywords.Zahl);
                    return new AST_Number(varValue);
            }

            return null;
        }

        private SyntaxKind NextToken()
        {
            lastToken = currentToken;
            return currentToken = lexer.Lex();
        }
        public void NextToken(Keywords identifier)
        {
            if (currentToken == null)
                return;
            if (currentToken.keyword.Equals(identifier))
                NextToken();
            else
                throw new Exception($"Expected {identifier}, but got {currentToken.keyword}");
        }
        private void NextToken(params Keywords[] identifier)
        {
            if (identifier.Contains(currentToken.keyword))
                NextToken();
            else
                throw new Exception($"Expected {string.Join(", ", identifier)}, but got {currentToken.keyword}");
        }
    }

    public class AST_Number : AbstractSyntaxTree
    {
        public AST_Number(object Value) 
        {
            this.Value = Value;
        }

        public object Value;
    }

    public class AST_Variable : AbstractSyntaxTree
    {
        public AST_Variable(object Value, string Name)
        {
            this.Value = Value;
            this.Name = Name;
        }

        public object Value;
        public string Name;
    }

    public class AST_Ausgabe : AbstractSyntaxTree
    {
        public AST_Ausgabe(object Value, string Name)
        {
            this.Value = Value;
            this.Name = Name;
        }

        public object Value;
        public string Name;
    }

    public class AbstractSyntaxTree
    {
        public AbstractSyntaxTree NextToken;
    }


}
