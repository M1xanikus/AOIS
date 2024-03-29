using System;
using System.Globalization;
using BinaryNumbers;
using Operations;
namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
           
            int choose = -1;
            while (choose != 0)
            {
                Console.WriteLine("Выберите операцию:\n1. Сложение\n2. Умножение\n3. Деление\n4. Сложение чисел по IEEE\n0. Выход");
                choose = Convert.ToInt32(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Console.WriteLine("Введите первое число:");
                        Binary a = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{a.Straight_binary}]\r\nОбратный код: [{a.Reverse_binary}]\r\nДополнительный код: [{a.Additional_binary}]\r\n");
                        Console.WriteLine("Введите второе число:");
                        Binary b = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{b.Straight_binary}]\r\nОбратный код: [{b.Reverse_binary}]\r\nДополнительный код: [{b.Additional_binary}]\r\n");
                        Operation op = new();
                        Binary c = new(op.Addition(a, b));
                        Console.WriteLine($"Результат сложения:\nДвоичное представление[{c.Straight_binary}]\nДесятичное:{c.Number}");
                        break;
                    case 2:
                        Console.WriteLine("Введите первое число:");
                        Binary a2 = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{a2.Straight_binary}]\r\nОбратный код: [{a2.Reverse_binary}]\r\nДополнительный код: [{a2.Additional_binary}]\r\n");
                        Console.WriteLine("Введите второе число:");
                        Binary b2 = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{b2.Straight_binary}]\r\nОбратный код: [{b2.Reverse_binary}]\r\nДополнительный код: [{b2.Additional_binary}]\r\n");
                        Operation op2 = new();
                        Binary c2 = new(op2.Multiplication(a2, b2));
                        Console.WriteLine($"Результат умножения:\nДвоичное представление[{c2.Straight_binary}]\nДесятичное:{c2.Number}");
                        break;
                    case 3:
                        //Console.WriteLine("Введите число:");
                        //string str = Console.ReadLine().Replace(".",",");
                        //Binary_fract a3 = new(Double.Parse(str));
                        //Console.WriteLine($"В десятичном формате:[{a3.Number}]");
                        //Console.WriteLine($"В двоичном формате:[{a3.Binary}]");
                        Console.WriteLine("Введите первое число:");
                        Binary a3 = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{a3.Straight_binary}]\r\nОбратный код: [{a3.Reverse_binary}]\r\nДополнительный код: [{a3.Additional_binary}]\r\n");
                        Console.WriteLine("Введите второе число:");
                        Binary b3 = new(Convert.ToInt32(Console.ReadLine()));
                        Console.WriteLine($"Прямой код: [{b3.Straight_binary}]\r\nОбратный код: [{b3.Reverse_binary}]\r\nДополнительный код: [{b3.Additional_binary}]\r\n");
                        Operation op3 = new();
                        Binary_fract c3 = new(op3.Division(a3, b3));
                        Console.WriteLine(c3.Binary);
                        Console.WriteLine(c3.Number);
                        break;
                    case 4:
                        Console.WriteLine("Введите первое число:");
                        Binary_IEEE a4 = new(Convert.ToSingle(Console.ReadLine().Replace(".",",")));
                        Console.WriteLine($"Бинарный код: [{a4.Binary}]\r\n");
                        Console.WriteLine("Введите второе число:");
                        Binary_IEEE b4 = new(Convert.ToSingle(Console.ReadLine().Replace(".", ",")));
                        Console.WriteLine($"Бинарный код: [{b4.Binary}]\r\n");
                        Operation op4 = new();
                        Binary_IEEE c4 = new(op4.Addition_float(a4, b4));
                        Console.WriteLine(c4.Binary);
                        Console.WriteLine(c4.Number);
                        break;


                }
            }
            }
    }
} 
