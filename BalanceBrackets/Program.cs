using System;
using System.Collections;
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

        static int calculate(Queue<string> expression)
        {
            Stack <int> stack = new Stack<int>();
            
            foreach (var item in expression)
            {
                if (isOperation(item[0]))
                { 
                    int op1 = stack.Pop();
                    int op2 = stack.Pop();
                    switch (item[0])
                    {
                        case '+': stack.Push(op1 + op2); break;
                        case '-': stack.Push(op2 - op1); break;
                        case '*': stack.Push(op1 * op2); break;
                        case '/': stack.Push(op2 / op1); break;
                    }
                }
                else
                    stack.Push(Convert.ToInt32(item));
            }
            return stack.Pop();
        }

        static int getPrioritet(char c)
        {
            int ind = oper.IndexOf(c);
            if (ind >= 0) { return prioritet[ind]; }
            return -1;
        }

        static bool isOperation(char c)
        {
            return oper.IndexOf(c) >= 0;
        }

        static void getAllFromStackToStartBack(Stack<char> stack, Queue<string> queue)
        {
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item == '(') return;
                queue.Enqueue(item.ToString());
            }
        }

        static void printQueue(Queue<string> queue)
        {
            Console.Write("!");
            foreach (string item in queue)
                Console.Write(item + "|");
            Console.WriteLine("!");
        }

        static Queue<string> getBackExpression(string expression)
        { 

            Queue<string> queueElements = getElementsOfExpression(expression);
            printQueue(queueElements);
            Queue<string> queueBackExpression = new Queue<string>();
            Stack<char> stackOperations = new Stack<char>();
            
            foreach (var item in queueElements)
            {
                if (item == "(") stackOperations.Push(item[0]);
                else if (item == ")")
                {
                    getAllFromStackToStartBack(stackOperations, queueBackExpression);
                }
                else if (isOperation(item[0]))
                {
                    while (stackOperations.Count > 0)
                    {
                        if (getPrioritet(stackOperations.Peek()) >= getPrioritet(item[0]))
                        {
                            queueBackExpression.Enqueue(stackOperations.Pop().ToString());
                        }
                        else break;
                    }
                    stackOperations.Push(item[0]);
                }
                else
                    queueBackExpression.Enqueue(item);
            }
            while(stackOperations.Count > 0)
                queueBackExpression.Enqueue(stackOperations.Pop().ToString());
            return queueBackExpression;
        }

        static Queue<string> getElementsOfExpression(string expression)
        {
            Queue<string> elementsOfExpression = new Queue<string>();
            string newNum = "";
            foreach (char item in expression)
            {
                if (char.IsDigit(item)) newNum += item;
                else
                {
                    if (newNum != "")
                    {
                        elementsOfExpression.Enqueue(newNum);
                        newNum = "";
                    }
                    elementsOfExpression.Enqueue(item.ToString());
                }
            }
            if (newNum != "") elementsOfExpression.Enqueue(newNum);
            return elementsOfExpression;
        }
        static void Main(string[] args)
        {
            string s = ")(";
            //Console.WriteLine(isBalanceOneTypeBrackets(s));
            //Console.WriteLine(isBalanceBrackets(s));
            Console.WriteLine(21 + 91 * (11 - 6123 / (12 + 38) + (5465 - 821)));
            s = "2^10+15*(7*3^2-8)";
            //s = "2+3*5";
            Console.WriteLine(s);
            Queue<string> expres = getBackExpression(s);
            printQueue(expres);
            Console.WriteLine(calculate(expres));
        }
    }
}
