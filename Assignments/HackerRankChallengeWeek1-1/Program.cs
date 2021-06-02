using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace HackerRankChallengeWeek1_1
{

    class Result
    {

        /*
        * Complete the 'simpleArraySum' function below.
        *
        * The function is expected to return an INTEGER.
        * The function accepts INTEGER_ARRAY ar as parameter.
        */

        public static int simpleArraySum(List<int> ar)
        {
            int sum = 0;
            for(int i = 0; i < ar.Count; i++)
            {
                sum += ar[i];
            }
            return sum;
        }

    }
    class Program
    {
        public static void Main(string[] args)
        {
            int arCount = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> ar = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arTemp => Convert.ToInt32(arTemp)).ToList();

            int result = Result.simpleArraySum(ar);

            using(TextWriter textWriter = new StreamWriter(Directory.GetCurrentDirectory() + @"\Output.txt", true))
            {
                textWriter.WriteLine(result);
            }
        }
    }
}
