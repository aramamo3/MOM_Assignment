using System;
using System.Collections.Generic;
using MASClient;

namespace Anand
{
       class Program
    {
        static void Main(string[] args)
        {
            Application.Initialize();

            Console.WriteLine();
            Console.WriteLine(" To quit please enter Q");

            while (true)
            {

            lblStartDate:
                Console.WriteLine();
                Console.Write(" Enter Start Period (mmm-yyyy) : ");

                string startperiod = Console.ReadLine();
                string result = string.Empty;
                DateTime dateStart;

                if (startperiod.ToLower() == "q") { Environment.Exit(0); }

                if (!MAS.ValidateMonthYear(startperiod, out dateStart))
                {
                    Console.WriteLine(" Invalid format");
                    goto lblStartDate;
                }

            lblEndDate:
                Console.Write(" Enter End Period (mmm-yyyy) : ");
                string endperiod = Console.ReadLine();
                DateTime dateEnd;

                if (endperiod.ToLower() == "q") { Environment.Exit(0); }

                if (!MAS.ValidateMonthYear(endperiod, out dateEnd))
                {
                    Console.WriteLine(" Invalid format");
                    goto lblEndDate;
                }

                if (dateStart > dateEnd)
                {
                    Console.WriteLine(" Invalid Date Range");
                    goto lblStartDate;
                }

                MAS mas = new MAS(Application.MASAPIURL, Application.MASAPIResourceID);
                List<MASClient.Record> records = mas.GetData(dateStart, dateEnd);

                Console.WriteLine();
                Console.WriteLine(" 1. Periods where higher FC Interest Rates against Bank Rates ");
                Console.WriteLine();

                Console.WriteLine(" for fc_fixed_deposits_3m : ");
                Console.WriteLine(" " + mas.CompareInterestRates(InterestRateType.fc_fixed_deposits_3m, records));

                Console.WriteLine();
                Console.WriteLine(" for fc_fixed_deposits_6m : ");
                Console.WriteLine(" " + mas.CompareInterestRates(InterestRateType.fc_fixed_deposits_6m, records));

                Console.WriteLine();
                Console.WriteLine(" for fc_fixed_deposits_12m : ");
                Console.WriteLine(" " + mas.CompareInterestRates(InterestRateType.fc_fixed_deposits_12m, records));

                Console.WriteLine();
                Console.WriteLine(" for fc_savings_deposits : ");
                Console.WriteLine(" " + mas.CompareInterestRates(InterestRateType.fc_savings_deposits, records));

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(" 2. Overall Average Interest Rates ");

                Console.WriteLine();
                Console.WriteLine(" for fc_fixed_deposits_3m     : " + mas.OverallAverage(InterestRateType.fc_fixed_deposits_3m, records));
                Console.WriteLine(" for banks_fixed_deposits_3m  : " + mas.OverallAverage(InterestRateType.banks_fixed_deposits_3m, records));
                Console.WriteLine(" for fc_fixed_deposits_6m     : " + mas.OverallAverage(InterestRateType.fc_fixed_deposits_6m, records));
                Console.WriteLine(" for banks_fixed_deposits_6m  : " + mas.OverallAverage(InterestRateType.banks_fixed_deposits_6m, records));
                Console.WriteLine(" for fc_fixed_deposits_12m    : " + mas.OverallAverage(InterestRateType.fc_fixed_deposits_12m, records));
                Console.WriteLine(" for banks_fixed_deposits_12m : " + mas.OverallAverage(InterestRateType.banks_fixed_deposits_12m, records));
                Console.WriteLine(" for fc_savings_deposits      : " + mas.OverallAverage(InterestRateType.fc_savings_deposits, records));
                Console.WriteLine(" for banks_savings_deposits   : " + mas.OverallAverage(InterestRateType.banks_savings_deposits, records));

                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine(" 3. Interest Rate Trend : " + mas.InterestRateTrend(records));

                goto lblStartDate;
            }
        }
    }

}
