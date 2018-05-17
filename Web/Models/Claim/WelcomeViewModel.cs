using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Model;


namespace Web.Models.Claim
{
    public class WelcomeViewModel
    {
        public IList<Rejected997Details> TransactionRejection { get; set; }
        public IList<Rejected997Details> ClaimRejection { get; set; }
        public IList<Rejected997Details> TransactionSummary { get; set; }

    }

    //public class RejectionDetails
    //{
    //    public string Transcount { get; set; }
    //    public string PostedDate { get; set; }
    //}

}