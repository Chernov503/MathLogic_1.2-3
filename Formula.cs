using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Formula
    {

        public string formula { get; set; }
        //Любой набор переменных
        public readonly string alphabit = "ABCDI";
        //любой набор нужных условных математических знаков
        public readonly string z = "+-=!";
        public Formula(string s)
        {
            formula = s.Replace(" ", "");
        }
        bool ScobSumm()
        {
            return formula.Count(x => x == '(') == formula.Count(x => x == ')');
        }

        bool IsPodformula(string s)
        {
            //проверка на скобки
            if (!s.StartsWith('(') || !s.EndsWith(')'))
            {
                Console.WriteLine("Формула написана некорректно");
                return false;
            }
            //основная проверка
            for (int i = 1; i < s.Length - 1; i++)
            {
                try
                {
                    //если символ - буква и до/после нее стоит буква
                    if (alphabit.Contains(s[i]))
                    {
                        if (alphabit.Contains(s[i - 1]) || alphabit.Contains(s[i + 1]))
                        {
                            Console.WriteLine("Между двумя элементами нет знака");
                            return false;
                        }
                    }
                    //если символ - знак и до/после него нет буквы
                    if (z.Contains(s[i]))
                    {
                        if (!alphabit.Contains(s[i - 1]) || !alphabit.Contains(s[i + 1]))
                        {
                            Console.WriteLine("Два знака стоят друг за другом");
                            return false;
                        }
                    }
                }
                catch { return false; }
            }
            return true;
        }
        public bool IsFormula()
        {
            string input = formula;
            //Если сумма скобок не совпадает, то false
            if (!ScobSumm()) { Console.WriteLine("Проверка на скобочный итог не пройдена"); return false; }

            string pattern = @"\(([^()]+)\)";
            bool exit = false;

            do
            {
                MatchCollection matches = Regex.Matches(input, pattern);

                foreach (Match match in matches)
                {
                    string podformula = match.Value;

                    //Проверка подформулы на корректность
                    if (!IsPodformula(podformula))
                    {
                        return false;
                    }

                    //Замена подформлы на I
                    Console.Write($"Заменим {podformula} на I");
                    input = input.Replace(podformula, "I");
                    Console.Write("\tФормула: " + input + "\n");

                }
                //проверка на то что идти дальше некуда
                exit = input == "I" || exit;
            } while (!exit);


            return true;
        }

        public void ConvertToPrefix()
        {
            var s = formula.Replace(" ", "").Split("");

        }
    }
}

