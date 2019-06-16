using System;
using System.Collections.Generic;
using System.Text;

namespace MASClient
{
    public class Record
    {
        public string end_of_month { get; set; }
        public string prime_lending_rate { get; set; }
        public string banks_fixed_deposits_3m { get; set; }
        public string banks_fixed_deposits_6m { get; set; }
        public string banks_fixed_deposits_12m { get; set; }
        public string banks_savings_deposits { get; set; }
        public string fc_hire_purchase_motor_3y { get; set; }
        public string fc_housing_loans_15y { get; set; }
        public string fc_fixed_deposits_3m { get; set; }
        public string fc_fixed_deposits_6m { get; set; }
        public string fc_fixed_deposits_12m { get; set; }
        public string fc_savings_deposits { get; set; }
        public string timestamp { get; set; }
    }

    public class Result
    {
        public List<string> resource_id { get; set; }
        public int limit { get; set; }
        public string total { get; set; }
        public List<Record> records { get; set; }
    }

    public class RootObject
    {
        public bool success { get; set; }
        public Result result { get; set; }
    }
}
