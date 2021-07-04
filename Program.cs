using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter you name:");
            var greeting = Console.ReadLine();
            Console.WriteLine($"Hello {greeting}!");
        }
    }
}
