

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Models.Claim;
using System.Web.Mvc;

namespace Web.Models.Repository
{
    public class DataRepository
    {
        // public static IList<TransactionModel> transactionData = null;
      
        private IList<MasterTransactionDispalyViewModel> transactionData = null;
      
        public IList<MasterTransactionDispalyViewModel> GetTransactionDetails()
        { 
            if (transactionData == null)
            {
                transactionData = new List<MasterTransactionDispalyViewModel>();

                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000004", Posted_File = "wS0gt56688888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000006", Posted_File = "6565656566.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Claim", Trans_Mode = "Batch", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000099", Posted_File = "TestData.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Claim", Trans_Mode = "Batch", Status_TA1 = 0, Status_997 = 0, Total = 15, Status_Summary = 1, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000101", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000102", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000103", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000104", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000105", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000106", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000107", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000108", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000109", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000110", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000111", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000112", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000113", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000114", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000115", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000116", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000117", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000118", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
                transactionData.Add(new MasterTransactionDispalyViewModel() { TransactionID = "1426PRV837D_000000119", Posted_File = "wS01585888888888888.txt", Posted_Date = "01/01/2014", EdiVestion = "4010A1", Trans_Type = "Eligibility", Trans_Mode = "Real", Status_TA1 = 0, Status_997 = 0, Status_Summary = 1, Total = 15, Accepted = 10, Rejected = 5 });
               
            }
            return transactionData;
        }

       
         

    }

}

