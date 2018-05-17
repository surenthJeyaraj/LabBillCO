using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class RejectedClaimDetail
    {
        public string TransactionNo { get; set; }
        public string ClaimNo { get; set; }
        public string LoopID { get; set; }
        public string SegmentID { get; set; }
        public string ElementID { get; set; }
        public string ErrorDescription { get; set; }
        public string EntryDate { get; set; }
    }
}
