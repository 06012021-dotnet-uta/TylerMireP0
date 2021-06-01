using System;

namespace helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Please enter age:");
            string userAge = Console.ReadLine();

            Console.WriteLine("Please enter name:");
            string userName = Console.ReadLine();


            Console.WriteLine($"Your name is {userName} and you are {userAge} year(s) old");
        }
    }
}
