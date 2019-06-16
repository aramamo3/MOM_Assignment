using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace MASClient
{
    public enum InterestRateType
    {
        prime_lending_rate,
        banks_fixed_deposits_3m,
        banks_fixed_deposits_6m,
        banks_fixed_deposits_12m,
        banks_savings_deposits,
        fc_hire_purchase_motor_3,
        fc_housing_loans_15y,
        fc_fixed_deposits_3m,
        fc_fixed_deposits_6m,
        fc_fixed_deposits_12m,
        fc_savings_deposits
    }

    public class MAS : APIBase.ClientInstance
    {
        public MAS(string uri, string resourceId) : base(uri, resourceId)
        { }

        public List<Record> GetData(DateTime startDate, DateTime endDate)
        {
            return GetData<RootObject>("filters[end_of_month]", BuildPeriodCollection(startDate, endDate)).Result.result.records;
        }

        public string CompareInterestRates(InterestRateType intType, IEnumerable<Record> records)
        {
            switch (intType)
            {
                case InterestRateType.fc_fixed_deposits_3m:
                    return string.Join(", ", records.Where(record => (float.Parse(record.fc_fixed_deposits_3m) > float.Parse(record.banks_fixed_deposits_3m))).Select(x => x.end_of_month));

                case InterestRateType.fc_fixed_deposits_6m:
                    return string.Join(", ", records.Where(record => (float.Parse(record.fc_fixed_deposits_6m) > float.Parse(record.banks_fixed_deposits_6m))).Select(x => x.end_of_month));

                case InterestRateType.fc_fixed_deposits_12m:
                    return string.Join(", ", records.Where(record => (float.Parse(record.fc_fixed_deposits_12m) > float.Parse(record.banks_fixed_deposits_12m))).Select(x => x.end_of_month));

                case InterestRateType.fc_savings_deposits:
                    return string.Join(", ", records.Where(record => (float.Parse(record.fc_savings_deposits) > float.Parse(record.banks_savings_deposits))).Select(x => x.end_of_month));

                default: return "Not part of the Compare Interest Rates List";
            }
        }

        public string OverallAverage(InterestRateType intType, IEnumerable<Record> records)
        {
            switch (intType)
            {
                case InterestRateType.fc_fixed_deposits_3m:
                    return records.Select(x => float.Parse(x.fc_fixed_deposits_3m)).Average().ToString();

                case InterestRateType.banks_fixed_deposits_3m:
                    return records.Select(x => float.Parse(x.banks_fixed_deposits_3m)).Average().ToString();

                case InterestRateType.fc_fixed_deposits_6m:
                    return records.Select(x => float.Parse(x.fc_fixed_deposits_6m)).Average().ToString();

                case InterestRateType.banks_fixed_deposits_6m:
                    return records.Select(x => float.Parse(x.banks_fixed_deposits_6m)).Average().ToString();

                case InterestRateType.fc_fixed_deposits_12m:
                    return records.Select(x => float.Parse(x.fc_fixed_deposits_12m)).Average().ToString();

                case InterestRateType.banks_fixed_deposits_12m:
                    return records.Select(x => float.Parse(x.banks_fixed_deposits_12m)).Average().ToString();

                case InterestRateType.fc_savings_deposits:
                    return records.Select(x => float.Parse(x.fc_savings_deposits)).Average().ToString();

                case InterestRateType.banks_savings_deposits:
                    return records.Select(x => float.Parse(x.banks_savings_deposits)).Average().ToString();

                default: return "Not part of the Overall Average List";
            }
        }

        public string InterestRateTrend(List<Record> records)
        {
            var sortedlist = records.OrderBy(x => x.end_of_month).ToList();

            int count = sortedlist.Count();
            int trend = 0;

            string result = "Stable";

            Record record1;
            Record record2;

            for (int i=0; i < count; i++)
            {
                record1 = sortedlist[i];

                if ((i + 1) >= count) break;
                record2 = sortedlist[i + 1];

                UpdateTrend(float.Parse(record2.prime_lending_rate), float.Parse(record1.prime_lending_rate), ref trend);
                UpdateTrend(float.Parse(record2.banks_fixed_deposits_3m), float.Parse(record1.banks_fixed_deposits_3m), ref trend);
                UpdateTrend(float.Parse(record2.banks_fixed_deposits_6m), float.Parse(record1.banks_fixed_deposits_6m), ref trend);
                UpdateTrend(float.Parse(record2.banks_fixed_deposits_12m), float.Parse(record1.banks_fixed_deposits_12m), ref trend);
                UpdateTrend(float.Parse(record2.banks_savings_deposits), float.Parse(record1.banks_savings_deposits), ref trend);

                UpdateTrend(float.Parse(record2.fc_hire_purchase_motor_3y), float.Parse(record1.fc_hire_purchase_motor_3y), ref trend);
                UpdateTrend(float.Parse(record2.fc_housing_loans_15y), float.Parse(record1.fc_housing_loans_15y), ref trend);
                UpdateTrend(float.Parse(record2.fc_fixed_deposits_3m), float.Parse(record1.fc_fixed_deposits_3m), ref trend);
                UpdateTrend(float.Parse(record2.fc_fixed_deposits_6m), float.Parse(record1.fc_fixed_deposits_6m), ref trend);
                UpdateTrend(float.Parse(record2.fc_fixed_deposits_12m), float.Parse(record1.fc_fixed_deposits_12m), ref trend);
                UpdateTrend(float.Parse(record2.fc_savings_deposits), float.Parse(record1.fc_savings_deposits), ref trend);
            }

            if (trend == 0) { result = "Stable"; }
            else if (trend > 0) { result = "Upward"; }
            else { result = "Downward"; }

            return result;
        }

        private static void UpdateTrend(float value1, float value2, ref int trend)
        {
            if (value1 != value2)
            {
                if (value2 > value1) { trend++; } else { trend--; }
            }
        }

        public static bool ValidateMonthYear(string data, out DateTime strDate)
        {
            strDate = DateTime.Now;

            try { strDate = DateTime.ParseExact(data, "MMM-yyyy", CultureInfo.InvariantCulture); }
            catch { return false; }

            return true;
        }

        public static string BuildPeriodCollection(DateTime startDate, DateTime endDate)
        {
            string collection = string.Empty;

            for (var current = startDate; current <= endDate; current = current.AddMonths(1))
            {
                collection += current.ToString("yyyy-MM,");
            }

            return collection.Remove(collection.Length - 1, 1);
        }
    }
}
