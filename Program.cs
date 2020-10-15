using CronMaker;
using System;

namespace CronJobMaker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CronStructure cron = new CronStructure();
            cron.Minutes.EveryBetween(2, 4);
            Console.WriteLine(cron);
        }
    }
}
