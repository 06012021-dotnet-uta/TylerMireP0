using System;

namespace helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hi!
            Console.WriteLine("Hello World!");

            //Prompt user for age
            Console.WriteLine("Please enter age:");
            int userAge = Int32.Parse(Console.ReadLine());

            //Verify that age is correct
            while(userAge > 120 || userAge < 0)
            {
                Console.WriteLine("Please enter valid age: ");
                userAge = Int32.Parse(Console.ReadLine());
            }

            //Prompt user for name
            Console.WriteLine("Please enter name:");
            string userName = Console.ReadLine();

            //Print to console using string interpolation
            Console.WriteLine($"Your name is {userName} and you are {userAge} year(s) old");
        }
    }
}
