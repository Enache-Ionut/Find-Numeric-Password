using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Find_Password
{
    class Program
    {
        static void Main(string[] args)
        {
            const long startInterval = 10000000;
            const long stopInterval = 99999999;
            const long intervalDimension = 69;

            string password = "14A1E34266BD2A5596A65265D05690E0E2724FD316830D147091BBE606E24DF3".ToLower();

            SplitInterval splitInterval = new SplitInterval(startInterval, stopInterval, intervalDimension);
            splitInterval.Split();

            CrackPassword crackPassword  = new CrackPassword(splitInterval.Intervals, password);
            crackPassword.FindPassword();

            var result = crackPassword.GetResult();

            Console.WriteLine("password : " + result.Key);
            Console.WriteLine("task : " + result.Value);

            Console.ReadLine();
        }
    }
}
