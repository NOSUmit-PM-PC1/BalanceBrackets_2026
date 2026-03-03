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
        static string oper = "+-*/";
        static string prioritet = "1122";
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

        static int getPrioritet(char c)
        {
            return 1;
        }

        static Queue<string> getBackExpressin(string expression)
        { 
            Queue<string> queueBackExpression = new Queue<string>();
            Stack<char> stackOperations = new Stack<char>();
            foreach (char item in expression)
            {
                if (item == '(') stackOperations.Push(item);
                else if (oper.IndexOf(item) >= 0)
                {
                    while (stackOperations.Count > 0)
                    {
                        if (stackOperations.Peek() == '(') { stackOperations.Push(item); break; }
                        else if (stackOperations.Peek() == ')')
                        {
                            while (stackOperations.Peek() != '(')
                            {
                                queueBackExpression.Append(stackOperations.Pop().ToString());
                            }
                            stackOperations.Pop();
                        }
                        else if (getPrioritet(stackOperations.Peek()) >= getPrioritet(item))
                        {
                            queueBackExpression.Append(stackOperations.Pop().ToString());
                        }
                        else
                        {
                            stackOperations.Push(item); break;
                        }
                    }
                }
                else
                    queueBackExpression.Append((item - 48).ToString());
            }
            while(stackOperations.Count > 0)
                queueBackExpression.Append(stackOperations.Pop().ToString());
            return queueBackExpression;
        }
        static void Main(string[] args)
        {
            //string s = ")(";
            //Console.WriteLine(isBalanceOneTypeBrackets(s));
            //s = "25 + 9 * (15- 16/(2+3)+[5-8]";
            //Console.WriteLine(isBalanceBrackets(s));
            Console.WriteLine(calculate("236*+"));
            Queue<string> expres = getBackExpressin("2+3*5");
            foreach (string item in expres) 
                Console.Write(item);
        }
    }
}
