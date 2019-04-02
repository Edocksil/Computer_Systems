using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            MultiplierRegisterRight multiplier = new MultiplierRegisterRight();
            int firstNumber = 0;
            int secondNumber = 0;
            Console.WriteLine("Множник в правій частині регістру");
            do
            {
                Console.Write("Ваше перше число (у межах Int32): ");
                firstNumber = Int32.Parse(Console.ReadLine());
            } while (secondNumber > Int32.MaxValue || secondNumber < Int32.MinValue);
            do
            {
                Console.Write("Ваше друге число (у межах Int32): ");
                secondNumber = Int32.Parse(Console.ReadLine());
            } while (secondNumber > Int32.MaxValue || secondNumber < Int32.MinValue);
            int[] result = multiplier.Multiply(firstNumber, secondNumber);

            Console.ReadKey();
        }
    }
    class MultiplierRegisterRight
    {
        public MultiplierRegisterRight()
        {

        }

        public string ConvertToPrint(int[] numberToPrint)
        {
            string number = "";
            for (int i = 0; i < numberToPrint.Length; i++)
            {
                number += numberToPrint[i].ToString();
            }
            return number;
        }

        public int[] Multiply(Int32 first, Int32 second)
        {
            int[] multiplicand = BinaryConverter(first);
            int[] multiplier = BinaryConverter(second);
            int[] product = new int[64];
            for (int i = 0; i < 32; ++i)
            {
                Console.WriteLine();
                Console.WriteLine("Крок (ітерація): " + (i + 1));
                Console.WriteLine("Перший множник (multiplicand): " + ConvertToPrint(multiplicand));
                Console.WriteLine("Другий множник (multiplier)  : " + ConvertToPrint(multiplier));

                if (multiplier[multiplier.Length - 1] == 1) //перевірка Least significant bit
                {
                    Console.WriteLine();
                    Console.WriteLine("//Додаємо перший множник до результату множення,\n//бо Найменш Істотний Біт (LSB) множника = 1");
                    product = Addition(multiplicand, product);
                    Console.WriteLine();
                    Console.WriteLine("Результат :" + ConvertToPrint(product));
                }
                else
                {
                    Console.WriteLine("\nLSB множника != 1, тому лише:");
                }
                Console.WriteLine();
                Console.WriteLine("Зсуваємо перший множник (multiplicand) вліво на 1 біт");
                multiplicand = ShiftLeftWithSign(multiplicand);
                Console.WriteLine("Зсуваємо другий множник (multiplier) вправо на 1 біт");
                multiplier = ShiftRight(multiplier);
                Console.WriteLine(new String('-', 50));
            }

            Console.WriteLine("У бінарному вигляді: ");
            Console.WriteLine(ConvertToPrint(BinaryConverter(first))
                + "\nХ (помножено на)\n" + ConvertToPrint(BinaryConverter(second))
                + "\n= (дорівнює)\n" + ConvertToPrint(product));

            Console.WriteLine(new String('-', 50));

            Console.WriteLine("У десятковому вигляді: ");
            Console.WriteLine(first + " x " + second + " = " + DecimalConverter(product));
            return product;
        }

        public int DecimalConverter(int[] binary)
        {
            if (binary[0] != 0)
            {
                binary = Addition(binary, BinaryConverter(-1));
                for (int i = 1; i < binary.Length; i++)
                {
                    binary[i] = (binary[i] == 0 ? 1 : 0);
                }
            }
            int number = 0;
            for (int i = binary.Length - 1; i >= 1; i--)
            {
                number += binary[i] * (int)Math.Pow(2, binary.Length - 1 - i);
            }
            return number;
        }
        public int[] BinaryConverter(int numberToConvert)
        {
            int[] binaryForm = new int[32];
            //визначення знаку
            if (numberToConvert < 0)
            {
                binaryForm[0] = 1;
            }
            else
            {
                binaryForm[0] = 0;
            }
            //прибираю знак
            numberToConvert = Math.Abs(numberToConvert);
            //перетворення у двійкову систему
            for (int i = 0; i < binaryForm.Length && numberToConvert != 0; i++)
            {
                binaryForm[binaryForm.Length - 1 - i] = numberToConvert % 2;
                numberToConvert /= 2;
            }
            return binaryForm;
        }
        //додавання двійкових чисел
        public int[] Addition(int[] first_mult, int[] second_product)
        {
            int[] number = new int[64];
            int temp = 0;
            for (int i = first_mult.Length - 1; i >= 1; i--)
            {
                int result = first_mult[i] + second_product[i + 32] + temp;
                switch (result)
                {
                    case 0:
                        number[i + 32] = result;
                        temp = 0;
                        break;
                    case 1:
                        number[i + 32] = result;
                        temp = 0;
                        break;
                    case 2:
                        number[i + 32] = 0;
                        temp = 1;
                        break;
                    case 3:
                        number[i + 32] = 1;
                        temp = 1;
                        break;
                }
            }
            return number;
        }

        public int[] ShiftRight(int[] number)
        {
            int previous_bit = number[0];
            int next_bit = number[0];
            number[0] = 0;
            //починаю з 2 біта зліва, бо 1 - знаковий, його не чіпаємо
            for (int i = 1; i < number.Length; i++)
            {
                next_bit = number[i];
                number[i] = previous_bit;
                previous_bit = next_bit;
            }
            return number;
        }

        public int[] ShiftLeftWithSign(int[] number)
        {
            int previous_bit = 0;
            int next_bit = 0;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                next_bit = number[i];
                number[i] = previous_bit;
                previous_bit = next_bit;
            }
            return number;
        }
    }

}
