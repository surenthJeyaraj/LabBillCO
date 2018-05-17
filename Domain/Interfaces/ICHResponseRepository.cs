using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Web.Models.Claim;

namespace Domain.Interfaces
{
    public interface ICHResponseRepository
    {
        // string getCHResponse(ClearinghouseResponse searchfield);
        string getTA1StatusCode(string TransactionID);
        string get997HTMLResponse(string TransactionID);
        string getCHResponse(string TransactionID, string CLaimRecID, string STRecID);
        string getSumarryReport(string TransactionID);
         List<Rejected997Details> GetTransactionReport();
        List<Rejected997Details> GetLabData();
        List<Rejected997Details> GetClaimRejectionReport();
        List<RejectedClaimDetail> GetClaimRejectionDetails(string transactionNo, string fromDate, string toDate);
        List<RejectedClaimDescription> GetClaimRejectionDescription(string transactionNo,string claimNo);
    }
}
