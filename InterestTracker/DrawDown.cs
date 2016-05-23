using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestTracker
{
    class DrawDown
    {
        public Guid DrawDownId { get; set; }
        public Guid LoanGuid { get; set; }
        public DateTime DrawDownDate { get; set; }
        public decimal DrawDownAmount { get; set; }

    }
}
