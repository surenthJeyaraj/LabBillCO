using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Claim;
namespace Web.Models.Claim
{
    public class MasterTransactionDispalyViewModel
    {

        public string TransactionID { get; set; }
        public string Posted_File { get; set; }
        public string Posted_Date { get; set; }
        public string EdiVestion { get; set; }
        public string Trans_Type { get; set; }
        public string Trans_Mode { get; set; }
        public int Status_TA1 { get; set; }
        public int Status_997 { get; set; }
        public int Status_Summary { get; set; }
        public int Total { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
      //  public IList<MasterTransactionDispalyViewModel> MasterTransactions { get; set; }


    }

    public class MasterTransactionDetailDispalyViewModel
    {
        public string TransactionID { get; set; }

        public string PatientName { get; set; }
        public string CLAIMID { get; set; }
        public string ProviderName { get; set; }
        public string DateProcessed { get; set; }
        public string ClaimType { get; set; }
        public string PatientAcct { get; set; }
        public string PayerID { get; set; }
        public string TaxID { get; set; } 
        public string DOS { get; set; }
        public string ClientStatus { get; set; }
        public string CHStatus { get; set; }
        public string PayerStatus { get; set; }
        public string ChargedAmt { get; set; }
        public IList<MasterTransactionDetailDispalyViewModel> DetailsTransactions { get; set; }

    }

}