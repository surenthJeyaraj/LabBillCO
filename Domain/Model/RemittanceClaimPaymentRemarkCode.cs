using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class RemittanceClaimPaymentRemarkCode
    {
        public int Id { get; set; }
        public int RemittanceServiceLineId { get; set; }
        public string QualifierCode { get; set; }
        public string RemarkCode { get; set; }
        public string RemarkCodeDescription { get; set; }
    }
}
