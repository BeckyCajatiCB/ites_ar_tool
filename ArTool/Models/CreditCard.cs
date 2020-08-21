using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArTool.Models
{
    public class CreditCard
    {
        public string Name { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string CardType { get; set; }

        public string Last4Digits { get; set; }

        public string First6Digits { get; set; }

        public string ExpirationMonth { get; set; }

        public string ExpirationYear { get; set; }
    }
}
