using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 同步執行
{
class Program
{
    static void Main(string[] args)
    {
        Console.Write(1);
        Level1();
        Console.Write(2);
    }

    private static void Level1()
    {
        Console.Write(3);
        Level2();
        Console.Write(4);
    }

    private static void Level2()
    {
        Console.Write(5);
    }
}
}
