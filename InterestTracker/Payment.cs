using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestTracker
{
    public class Payment
    {
        public Guid LoanGUID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public decimal InterestPaymentAmount { get; set; }
        public decimal PrincipalPaymentAmount { get; set; }
    }
}
