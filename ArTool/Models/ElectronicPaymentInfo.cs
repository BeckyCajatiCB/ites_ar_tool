using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArTool.Models
{
    public class ElectronicPaymentInfo
    {
        public string AccountDid { get; set; }

        public string PaymentType { get; set; }

        public string TokenId { get; set; }

        public string TokenExpireDt { get; set; }

        public string CreatedById { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}
