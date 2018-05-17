using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Remittance
{
    public class RemittanceFilters
    {
        public string OfficeName { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ClaimNo { get; set; }
        public string PayerName { get; set; }
        public string CheckNo { get; set; }
    }
}