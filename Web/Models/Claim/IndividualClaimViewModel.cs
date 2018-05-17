using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.Claim
{
    public class IndividualClaimViewModel
    {
        public string PatientName { get; set; }
        public string CLAIMID { get; set; }
        public string ProviderName { get; set; }
        public string DateProcessed { get; set; }
        public string ClaimType { get; set; }
        public string PatientAcct { get; set; }
        public string PayerID { get; set; }
        public string TaxID { get; set; }
        public string ClientID { get; set; }
        public string DOS { get; set; }
        public string ClientStatus { get; set; }
        public string CHStatus { get; set; }
        public string PayerStatus { get; set; }
        public string ChargedAmt { get; set; }
        public IEnumerable<MasterTransactionDetailDispalyViewModel> DetailTransactions { get; set; }
    }
}