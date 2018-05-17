using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class RemittanceClaimPaymentReasonCode
    {
        public int Id { get; set; }
        public int PaymentAndServiceLineId { get; set; }
        public string GroupCode { get; set; }
        public string GroupCodeDescription { get; set; }
        public string ReasonCode { get; set; }
        public string ReasonCodeDescription { get; set; }
        public decimal? Amount { get; set; }
        public string LoopLevel { get; set; }
    }
}
