using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main()
        {
            string input = "(A^(A / A * B) + (A+(A-A)))";
            Console.WriteLine($"Лабораторная 1. 3 Вариант (Задание 1,2)\n\n" +
                $"Соблюдайте заданный формат входных данных \n" +
                $"(Всю формулу необходимо заключить в скобки)\n" +
                $"Допустимые переменные: ABCD (возможно изменить в коде)\n" +
                $"Дпустимые символы */-><=+%&^#@! (возможно изменить в коде)\n" +
                $"Пример: {input}\n");

            var formula = new Formula(input);
            Console.WriteLine($"Предполагаемая формула {input}\n");
            bool b = formula.IsFormula();
            Console.WriteLine($"Формула {b}");


            input = Console.ReadLine();
            formula = new Formula(input);
            Console.WriteLine($"Предполагаемая формула {input}\n");
            b = formula.IsFormula();
            Console.WriteLine($"Формула {b}");


        }
    }
}