using System.Text;
using System.Threading;

namespace HillelHomeWorks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            //Task 1
            //Знайти позицію літери в алфавіті та перевести її в інший регістр
            Console.WriteLine("Task 1");
            Console.WriteLine("Enter a char letter");
            string strch = Console.ReadLine();
            if (strch != null && strch != "" && strch.Length == 1)
            {
                char ch = strch[0];
                int chint = ch;
                int index, newch;
                if (chint > 64 && chint < 91)
                {
                    index = chint - 64;
                    newch = index + 96;
                    Console.WriteLine($"Input char {ch} have num on alphabet is {index} and he become {(char)newch}");
                }
                else if (chint > 96 && chint < 123)
                {
                    index = chint - 96;
                    newch = index + 64;
                    Console.WriteLine($"Input char {ch} have num on alphabet is {index} and he become {(char)newch}");
                }
                else
                    Console.WriteLine("Incorrect char");
            }
            else
                Console.WriteLine("Incorrect char");
            //----------------------------------------------------------------------------
            //Task 2
            //Розділювач рядка. Дана строка та символ, потрібно розділити строку на кілька строк(масив строк) 
            //    виходячи із заданого символу. Наприклад: строка = "Лондон, Париж, Рим", а символ = ','.
            //    Результат = string[] { "Лондон", "Париж", "Рим" }.
            Console.WriteLine("Task 2");
            string str = "Python! JS! C#? C++! Java";
            char ch1 = '!';
            int size = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch1)
                    size++;
            }
            string[] strings = new string[++size];
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch1)
                    j++;
                else
                {
                    if (str[i] != ' ')
                        strings[j] += str[i];
                }
            }
            Console.WriteLine($"Start String {str} and separator {ch1}. Ended array:");
            foreach (var strng in strings)
                Console.WriteLine(strng);
            //Task 3
            //Пошук підстроки у строці.
            Console.WriteLine("Task 3");
            string startstr = "I love coding on c#";
            string searchstr = "ing on";
            bool find = false;
            for (int i = 0; i < startstr.Length; i++)
            {
                if (startstr[i] == searchstr[0])
                {
                    bool check = true;
                    int k = i;
                    for (int l = 0; l < searchstr.Length && k < startstr.Length; l++)
                    {
                        if (startstr[k] == searchstr[l])
                            k++;
                        else
                            check = false;
                    }
                    if (check)
                        find = true;
                }
            }
            if (find)
                Console.WriteLine($"Substring {searchstr} was found on {startstr}");
            else
                Console.WriteLine($"Substring {searchstr} was not found on {startstr}");
            //Task 4
            //Написати програму, яка виводить число літерами. Приклад: 117 - сто сімнадцять
            Console.WriteLine("Task 4");
            Console.WriteLine("Enter a number:");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine(NumberToText(number));
            //Task 4
            //Поміняти місцями значення двох змінних (типу int) (без використання 3й)
            Console.WriteLine("Task 5");
            int one = 11;
            int two = 22;
            //Variant 1
            Console.WriteLine($"First num {one}; Second num {two}");
            int[] ints = new int[] { one, two };
            one = ints[1];
            two = ints[0];
            Console.WriteLine($"Change num\nFirst num {one}; Second num {two}");
            //Variant 2
            string path = @"C:/Users/mishy/source/repos/HillelHomeWorks/HillelHomeWorks/HW1.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLineAsync(one.ToString());
                writer.WriteLineAsync(two.ToString());
            }
            using (StreamReader reader = File.OpenText(path))
            {
                two = int.Parse(reader.ReadLine());
                one = int.Parse(reader.ReadLine());
            }
            Console.WriteLine($"Change num\nFirst num {one}; Second num {two}");
            Console.ReadLine();
        }
        static string NumberToText(int number)
        {
            if (number == 0)
                return "нуль";

            if (number < 0)
                return "мінус " + NumberToText(-number);

            string[] units = { "", "один", "два", "три", "чотири", "п'ять", "шість", "сім", "вісім", "дев'ять", "десять",
                           "одинадцять", "дванадцять", "тринадцять", "чотирнадцять", "п'ятнадцять", "шістнадцять",
                           "сімнадцять", "вісімнадцять", "дев'ятнадцять" };

            string[] tens = { "", "", "двадцять", "тридцять", "сорок", "п'ятдесят", "шістдесят", "сімдесят", "вісімдесят", "дев'яносто" };

            string[] hundreds = { "", "сто", "двісті", "триста", "чотириста", "п'ятсот", "шістсот", "сімсот", "вісімсот", "дев'ятсот" };
            
            string thous = "тисяч";

            List<string> parts = new List<string>();

            if (number >= 20000)
            {
                parts.Add(tens[number / 10000]);
                number %= 10000;
                if (number.ToString().Length<4)
                    parts.Add(thous);
            }

            if (number >= 1000)
            {
                parts.Add(units[number / 1000]);
                parts.Add(thous);
                number %= 1000;
            }

            if (number >= 100)
            {
                parts.Add(hundreds[number / 100]);
                number %= 100;
            }

            if (number >= 20)
            {
                parts.Add(tens[number / 10]);
                number %= 10;
            }

            if (number > 0)
            {
                parts.Add(units[number]);
            }

            return string.Join(" ", parts).Trim();
        }
    }
}
