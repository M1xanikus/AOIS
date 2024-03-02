using BinaryNumbers;
using Operations;
namespace Menu
{
    class Menu
    {
        static void Main(string[] args)
        {
            //Binary_fract a = new Binary_fract(3.25);
            //Console.WriteLine(a.Binary);
            //Binary_fract b = new Binary_fract(a.Binary);
            //Console.WriteLine(b.Number);
            //Binary a = new Binary(9);
            //Console.WriteLine(a.Straight_binary);
            //Binary b = new Binary(7);
            //Console.WriteLine(b.Straight_binary);
            //Operation op = new Operation();
            //string c = op.Multiplication(a, b);
            //Binary cb = new Binary(c);
            //Console.WriteLine(c);
            //Console.WriteLine(cb.Number);
            //Console.WriteLine(a.Straight_binary);
            //Console.WriteLine(a.Reverse_binary);
            //Console.WriteLine(a.Additional_binary);
            //Console.WriteLine(a.Make_Decimal());
            int choose = -1;
            while (choose != 0)
            {
                Console.WriteLine("Выберите операцию:\n1. Сложение\n2. Умножение\n3. Деление\n0. Выход");
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
                }
            }
            }
    }
}