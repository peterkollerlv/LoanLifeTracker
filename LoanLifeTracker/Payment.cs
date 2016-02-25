using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanLifeTracker
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public decimal InterestPaymentAmount { get; set; }
        public decimal PrincipalPaymentAmount { get; set; }
    }
}
