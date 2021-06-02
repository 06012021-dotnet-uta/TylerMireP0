using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Collections.Generic;

namespace HackerRankChallengeWeek1_3
{
    class Result
    {

        /*
        * Complete the 'hourglassSum' function below.
        *
        * The function is expected to return an INTEGER.
        * The function accepts 2D_INTEGER_ARRAY arr as parameter.
        */

        public static int hourglassSum(List<List<int>> arr)
        {
            int highestHourglassSum = 0;
            for(int x = 0; x < arr.Count - 2; x++)
            {
                for(int y = 0; y < arr.Count - 2; y++)
                {
                    int sumOfHourglass = 0;
                    
                    //Top 3
                    sumOfHourglass += arr[x][y];
                    sumOfHourglass += arr[x][y + 1];
                    sumOfHourglass += arr[x][y + 2];

                    //Middle center
                    sumOfHourglass += arr[x + 1][y + 1];

                    //Bottom 3
                    sumOfHourglass += arr[x + 2][y];
                    sumOfHourglass += arr[x + 2][y + 1];
                    sumOfHourglass += arr[x + 2][y + 2];


                    //Grab the highest hourglass sum
                    if(sumOfHourglass > highestHourglassSum) highestHourglassSum = sumOfHourglass;
                }
            }
            return highestHourglassSum;
        }

    }

    class Solution
    {
        public static void Main(string[] args)
        {
            TextWriter textWriter = new StreamWriter(Directory.GetCurrentDirectory() + @"\Output.txt", true);

            List<List<int>> arr = new List<List<int>>();

            for (int i = 0; i < 6; i++)
            {
                arr.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList());
            }

            int result = Result.hourglassSum(arr);

            textWriter.WriteLine(result);

            textWriter.Flush();
            textWriter.Close();
        }
    }
}
