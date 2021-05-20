using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImportacaoDePrecos.Models
{
    public class TimeSeriesDaily
    {
        [JsonProperty("Time Series (Daily)")]
        public Days days { get; set; }
    }

    public class Days : Dictionary<string, Day> { }

    public class Day
    {
        [JsonProperty("4. close")]
        public double close { get; set; }
    }
}
