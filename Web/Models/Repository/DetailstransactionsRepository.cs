using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Claim;
using System.Web.Mvc;

namespace Web.Models.Repository
{
    public class DetailstransactionsRepository
    {
        private IList<MasterTransactionDetailDispalyViewModel> transactionDeatailsData = null;
        public IList<MasterTransactionDetailDispalyViewModel> GetTransactionDeatailsDetails()
        {
            if (transactionDeatailsData == null)
            {
                transactionDeatailsData = new List<MasterTransactionDetailDispalyViewModel>();

                transactionDeatailsData.Add(new MasterTransactionDetailDispalyViewModel() { TransactionID = "1426PRV837D_000000004", PatientName = "Donald", CLAIMID = "1426PRV837D_000000004", ProviderName = "01/01/2014", DateProcessed = "4010A1", ClaimType = "Eligibility", PatientAcct = "Test", PayerID = "DDVADemo", TaxID = "1", DOS = "15", ClientStatus = "10", CHStatus = "5" });
                 
            }
            return transactionDeatailsData;
        }
    }
}