using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceBrackets
{
    internal class Program
    {
        static bool isBalanceOneTypeBrackets(string s)
        {
            int balance = 0;

            foreach (char b in s)
            {
                if (b == '(') balance++;
                else if (b == ')') balance--;
                if (balance < 0) return false;
            }
            return balance == 0;
        }

        static bool isBalanceBrackets(string s)
        {
            string openBrackets = "<[{(";
            string closeBrackets = ">]})";
            Stack<char> stack = new Stack<char>();
            foreach (char b in s)
            {
                if (openBrackets.IndexOf(b) != -1) stack.Push(b);
                else
                {
                    if (stack.Count == 0) return false;
                    var topStack = stack.Pop();
                    if (openBrackets.IndexOf(topStack) != closeBrackets.IndexOf(b))
                        return false;
                }
            }
            return stack.Count == 0;
        }

        static int calculate(string s)
        {
            Stack <int> stack = new Stack<int>();
            string oper = "+-*/";
            foreach (char b in s)
            {
                if (oper.IndexOf(b) >= 0)
                { 
                    int op1 = stack.Pop();
                    int op2 = stack.Pop();
                    switch (b)
                    {
                        case '+': stack.Push(op1 + op2); break;
                        case '-': stack.Push(op2 - op1); break;
                        case '*': stack.Push(op1 * op2); break;
                        case '/': stack.Push(op2 / op1); break;
                    }
                }
                else
                    stack.Push(b - 48);
            }
            return stack.Pop();
        }
        static void Main(string[] args)
        {
            //string s = ")(";
            //Console.WriteLine(isBalanceOneTypeBrackets(s));
            //s = "25 + 9 * (15- 16/(2+3)+[5-8]";
            //Console.WriteLine(isBalanceBrackets(s));
            Console.WriteLine(calculate("236*+"));
        }
    }
}
