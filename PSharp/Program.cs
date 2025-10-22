using PSharp;
using System.Diagnostics;
using System.Runtime.InteropServices;

Lexer lexer = new Lexer("ausgabe 100.");

SyntaxKind kind = null;
Parser parser = new Parser(lexer);

void printNext(AbstractSyntaxTree astItem)
{
    if(astItem is AST_Variable a)
    {
        Console.WriteLine(a.Name + ": " + a.Value);
    }
    if(astItem is AST_Ausgabe b)
    {
        Console.WriteLine("AST Ausgabe");

        foreach (var item in b.Value as List<AbstractSyntaxTree>)
        {
            Console.WriteLine("\t" + item);
            printNext(item);
        }
    }
    if(astItem is AST_Number c)
    {
        Console.WriteLine("Value:" + c.Value);
    }
}

var root = parser.Parse();
while (root.NextToken != null)
{
    printNext(root.NextToken);
    root = root.NextToken;
}



Console.ReadLine();

