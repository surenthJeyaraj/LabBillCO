using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Claim;

namespace Web.Models.Claim
{
    public class TransactionModel
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


        public string ClaimsubmittedID { get; set; }
        public string CaparioTraceID { get; set; }
        public string InsuredID { get; set; }
        public string PatientAccount { get; set; }
        public string BillingPrvTaxId { get; set; }
        public string BillingPrvNpi { get; set; }
        public string BillingPrvLName { get; set; }
        public string BillingPrvFName { get; set; }
        public string CaparioProcessinStatus { get; set; }
        public string PayerProcessinStatus { get; set; }
        public string ClientSubmitterId { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }

        public IList<TransactionModel> Transactions { get; set; }
    }
   


}