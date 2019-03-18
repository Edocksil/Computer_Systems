using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir_address = @"D:\UNIV\Comp_Sys\lab_1\";
            string[] file_name = new string[] { "scp_1.txt", "brake_2.txt", "rick_3.txt" };
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            char[] alphabet = new char[33] {'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й',
                    'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
                    'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };

            for (int f = 0; f < file_name.Length; f++)
            {
                try
                {
                    double[] numberOfOccurence = new double[alphabet.Length];
                    double frequency = 0;
                    double entropy = 0;
                    int amount_of_letters = 0;

                    string file_address = dir_address + file_name[f];
                    FileInfo file = new FileInfo(file_address);
                    Console.WriteLine("Файл для аналізу: " + file.Name);
                    using (StreamReader sr = new StreamReader(file_address))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            for (int i = 0; i < alphabet.Length; i++)
                            {
                                var count = line.Count(x => x == alphabet[i]);
                                numberOfOccurence[i] += count;
                            }
                            amount_of_letters += line.Count(Char.IsLetter);
                        }
                    }
                    Console.WriteLine("Загальна кількість символів файлу: " + amount_of_letters);
                    Console.WriteLine();
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        frequency = numberOfOccurence[i] / amount_of_letters;
                        Console.WriteLine("Відносна частота появи літери \"" + alphabet[i] + "\" у тексті = " + frequency +
                            " ; Літера присутня у тексті: " + numberOfOccurence[i] + " разів.");
                        if (frequency != 0)
                        {
                            entropy += -(frequency * Math.Log(frequency, 2));
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Середня ентропія нерівноймовірного алфавіту у заданому тексті: " + entropy);
                    Console.WriteLine("Кількість інформації у тексті: " + (entropy * amount_of_letters) / 8);
                    if ((entropy * amount_of_letters) / 8 > amount_of_letters)
                    {
                        Console.WriteLine("");
                    }
                    else if ((entropy * amount_of_letters) / 8 < amount_of_letters)
                    {
                        Console.WriteLine("");
                    }
                    else
                    {
                        Console.WriteLine("Кількість інформації у тексті така ж, як і розмір файлу: ");
                    }
                    Console.ReadKey();
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
