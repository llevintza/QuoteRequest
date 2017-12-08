using System.Collections.Generic;

namespace QuoteRequest.Models
{
    public class QuoteRequest
    {
        public string Broker { get; set; }

        public string InsuredName { get; set; }

        public string Trade { get; set; }

        public List<Cover> Covers { get; set; }

        public decimal Revenue { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}