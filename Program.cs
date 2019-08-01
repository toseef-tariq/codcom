using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Data;
namespace program
{

    class Test
    {
       public static string[] one = new String[100];
       public static string[] two = new String[100]; 
       public static int count = 0;
       public void lexer(String token)
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();
            tokens.Add("if", "Keyword_if"); tokens.Add("else if", "Keyword_else_if"); tokens.Add("else", "Keyword_else"); tokens.Add("while", "Keyword_while"); tokens.Add("cout", "Keyword_cout"); tokens.Add("cin", "Keyword_cin");
            tokens.Add("return", "Keyword_return"); tokens.Add("char", "Keyword_char"); tokens.Add("int", "Keyword_int"); tokens.Add("(", "LeftParen"); tokens.Add(")", "RightParen");
            tokens.Add("{", "LeftBrace"); tokens.Add("}", "RightBrace"); tokens.Add(";", "Semicolon"); tokens.Add("<<", "LeftAngl"); tokens.Add(">>", "RightAngl"); tokens.Add("*", "Op_multiply");
            tokens.Add("/", "Op_divide"); tokens.Add("%", "Op_mod"); tokens.Add("+", "Op_add"); tokens.Add("-", "Op_subtract"); tokens.Add("<", "Op_less"); tokens.Add(">", "Op_greater");
            tokens.Add("<=", "Op_lessequal"); tokens.Add(">=", "Op_greaterequal"); tokens.Add("==", "Op_equal"); tokens.Add("!=", "Op_notequal"); tokens.Add("!", "Op_not"); tokens.Add("=", "Op_assign");
            tokens.Add("&&", "Op_and"); tokens.Add("||", "Op_or");
            if (token.Contains("\""))
                symbolTable("Msg", token);
            else
            {
                string[] res = Regex.Split(token, @"\s+");
                foreach (string word in res)
                {
                    token = word;
                    int n = token.IndexOf('_');
                    if (tokens.ContainsKey(token))
                        symbolTable(token, tokens[token]);
                    else if (n != -1)
                        symbolTable(token, "Identifier");
                    else
                    {
                        string patern = @"[0-9]+";
                        Regex rgx = new Regex(patern);
                        foreach (Match match in rgx.Matches(token))
                            symbolTable(match.Value, "Number");
                    }
                }
            }
        }
       public void symbolTable( string token, string value)
       {
          
           int index=Array.IndexOf(one, token);
           if ( index>-1)
               Console.WriteLine("{0,-10} {1,5} \t {2,-10} {3,5}\n", count, token, index, value);
           else
               Console.WriteLine("{0,-10} {1,5} \t {2,-10} {3,5}\n", count, token, count, value);
           one[count] = token;
           two[count] = value;
           count++;
       }  
    }
    public class Program
    {

        public static void Main(string[] args)
        {
            string text = File.ReadAllText(@"D:\program.txt");
            string pattern = @"(;)|(<<)|(>>)|(>=)|(<=)|(!=)|(!)|(=)|(>)|(<)|(&&)|(\|)|(\()|(\))|({)|(})";
            Console.WriteLine("{0,-10} {1,5} \t {2,-10} {3,5}\n", "Index",  "Value", "Refrence", "Description");
            foreach (string result in Regex.Split(text, pattern))
            {               
                 string input=result;
                 input = Regex.Replace(result, @"^\s+", "");
                 input = Regex.Replace(input, @"\s+$", "");                         
                 var t = new Test();
                 t.lexer(input);
            }
            Console.ReadLine();
        }
    }
}