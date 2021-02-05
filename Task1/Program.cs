using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

/*
 * 
 * Для решения задачи был использован алгоритм Обратной Польской Записи
 * 
 */


namespace Tasks
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nВведите выражение");

            Console.Write("> ");
            string expression;
            while ((expression = Console.ReadLine()) != "q")
            {

                try
                {
                    Console.WriteLine($"\n{expression} = {CalculateExpression(expression)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }

                Console.WriteLine("\nВведите выражение");

                Console.Write("> ");
            }
        }

        /// <summary>
        /// Расчитать выражение
        /// </summary>
        /// <param name="input">Строка выражения</param>
        /// <returns></returns>
        public static double CalculateExpression(string input)
        {

            CheckExpression(input);
            string resultStr = GetExpression(input.Replace('.', ','));
            return Calc(resultStr);
        }

        // Проверка правильности вводной строки
        static void CheckExpression(string input)
        {
            Regex regex = new Regex("[a-zA-Zа-яА-Я]");
            Match regexResult = regex.Match(input);
            if (regexResult.Success)
                throw new ArgumentException($"В выражении {input} имеется символ {regexResult.Value}.");
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (IsOperator(input[i], true) && IsOperator(input[i + 1], true))
                {
                    throw new ArgumentException($"В выражении {input} дважды повторяется оператор.");
                }

            }
        }


        
        // Финальный расчёт
        static double Calc(string parsedExpression)
        {
            double result = 0d;
            Stack<double> tmp = new Stack<double>();
            for (int i = 0; i < parsedExpression.Length; i++)
            {
                if (Char.IsDigit(parsedExpression[i]))
                {
                    string value = "";
                    while (parsedExpression[i] != ' ' && !IsOperator(parsedExpression[i]))
                    {
                        value += parsedExpression[i];
                        i++;
                        if (i == parsedExpression.Length) break;
                    }
                    tmp.Push(double.Parse(value));
                    i--;
                }
                else if (IsOperator(parsedExpression[i]))
                {
                    double a = tmp.Pop();
                    double b = tmp.Pop();

                    switch (parsedExpression[i])
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/':
                            {
                                if (a == 0)
                                    throw new ArgumentException("В выражении происходит деление на ноль.");
                                result = b / a; break;
                            }
                    }
                    tmp.Push(result);
                }
            }


            return tmp.Peek();
        }

        // Определение действий и последовательности чисел, согласно приоритету операторов
        static string GetExpression(string expression)
        {
            StringBuilder result = new StringBuilder();
            Stack<char> operatorStack = new Stack<char>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (Char.IsDigit(expression[i]))
                {
                    while (!IsOperator(expression[i]))
                    {
                        result.Append(expression[i]);
                        i++;
                        if (i == expression.Length) break;
                    }
                    result.Append(' ');
                    i--;
                    continue;
                }
                if (IsOperator(expression[i]))
                {
                    if (expression[i] == '(')
                        operatorStack.Push(expression[i]);
                    else if (expression[i] == ')')
                    {
                        char pop = operatorStack.Pop();
                        while (pop != '(')
                        {
                            result.Append(pop);
                            pop = operatorStack.Pop();
                        }
                    }
                    else
                    {
                        if (operatorStack.Count > 0)
                        {
                            if (GetPriority(expression[i]) <= GetPriority(operatorStack.Peek()))
                                result.Append(operatorStack.Pop());
                        }


                        operatorStack.Push(expression[i]);
                    }
                }
            }
            while (operatorStack.Count > 0)
            {
                result.Append(operatorStack.Pop());
            }
            return result.ToString();
        }


        static bool IsOperator(char symbol, bool ignoreBrackets = false)
        {
            return ignoreBrackets ? (("+-*/").IndexOf(symbol) != -1) : (("+-*/()").IndexOf(symbol) != -1);
        }


        static int GetPriority(char oper)
        {
            switch (oper)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 3;
                case '/': return 3;
                default: return -1;
            }
        }
    }
}
