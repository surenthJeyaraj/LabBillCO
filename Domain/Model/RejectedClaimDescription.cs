using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class RejectedClaimDescription
    {
        public string TransactionNo { get; set; }
        public string ClaimNo { get; set; }
        public string LoopId { get; set; }
        public string SegmentId { get; set; }
        public string ElementId { get; set; }
        public string ErrorDescription { get; set; }
       // public string EntryDate { get; set; }
    }
}
