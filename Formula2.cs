using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;
 
/*
 * 
 */
namespace ConsoleApp1
{
    public class Formula2
    {
        string Formula;
        Expression e;
        string alphabit = "ABCD";

        List<string> Elements = new List<string>();

        public Formula2(string s)
        {
            Formula = s;
            try { e = new Expression(Formula); }
            catch (Exception err) { Console.WriteLine($"Не формула. Ошибка: {err.Message}"); }

            GetParametersValue();

            try {
                bool result = Convert.ToBoolean(e.Evaluate());

                Console.WriteLine($"Является формулой\nОтвет: {result}");
            }
            catch (Exception err) { Console.WriteLine($"Не формула. Ошибка: {err.Message}"); }

        }

        void GetParametersValue()
        {
            //перебираем формулу
           foreach(var el in Formula)
            {
                //если символ из алфавита
                if (alphabit.Contains(el) && !Elements.Contains(el.ToString())) 
                {
                    //добавить в список
                    Elements.Add(el.ToString());
                    //получить от пользователя значение
                    Console.WriteLine($"Введите значение для {el} t/f");
                    do
                    {
                        switch(Console.ReadLine())
                        {
                            case "t":
                                {
                                    e.Parameters[el.ToString()] = true;
                                    break;
                                }
                            case "f":
                                {
                                    e.Parameters[el.ToString()] = false;
                                    break;
                                }
                        }
                    } while (e.Parameters[el.ToString()] == null);
                }
            }
        }






        public string ConvertInfixToPrefix(string infixExpression)
        {
            try
            {
                ValidateInfixExpression(ref infixExpression);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid infix expression.Error Details:{ 0}", ex.Message);
                return null;
            }

            Stack operatorStack = new System.Collections.Stack();
            Stack operandStack = new Stack();

            operatorStack.Push('(');
            infixExpression += ')';

            foreach (char ch in infixExpression)
            {
                if (ch == '(')
                {
                    operatorStack.Push(ch);
                }
                else if (ch == ')')
                {
                    char poppedOperator = (char)operatorStack.Pop();
                    while (poppedOperator != '(')
                    {
                        operandStack.Push(PrefixExpressionBuilder(operandStack, poppedOperator));
                        poppedOperator = (char)operatorStack.Pop();
                    }
                }
                else if (IsOperator(ch))
                {
                    char poppedOperator = (char)operatorStack.Pop();
                    bool sameOrHighPrecedence = CheckSameOrHighPrecedence(poppedOperator, ch);
                    while (sameOrHighPrecedence)
                    {
                        operandStack.Push(PrefixExpressionBuilder(operandStack, poppedOperator));
                        poppedOperator = (char)operatorStack.Pop();
                        sameOrHighPrecedence = CheckSameOrHighPrecedence(poppedOperator, ch);
                    }

                    operatorStack.Push(poppedOperator);
                    operatorStack.Push(ch);

                }
                else
                {
                    operandStack.Push(ch.ToString());
                }
            }
            return (string)operandStack.Pop();
        }


        private void ValidateInfixExpression(ref string expression)
        {
            expression = expression.Replace(" ", string.Empty);

        }


        private bool IsOperator(char character)
        {
            if ((character == '+') || (character == '-') || (character == '*') || (character == '/'))
            {
                return true;
            }
            return false;
        }
        private bool CheckSameOrHighPrecedence(char elementToTest, char checkAgainst)
        {
            bool flag = false;
            switch (elementToTest)
            {
                case '/':
                case '*':
                    flag = true;
                    break;
                case '+':
                case '-':
                    if ((checkAgainst == '+') || (checkAgainst == '-'))
                    {
                        flag = true;
                    }
                    break;
                default: 
                    flag = false;
                    break;
            }
            return flag;
        }

        private string PrefixExpressionBuilder(Stack operandStack, char operatorChar)
        {
            string operand2 = (string)operandStack.Pop();
            string operand1 = (string)operandStack.Pop();
            string infixExpression = string.Format($"{operatorChar}{operand1}{operand2}");

            return infixExpression;
        }


    }
}
