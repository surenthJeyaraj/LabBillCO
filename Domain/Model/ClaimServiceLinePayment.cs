using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace Domain.Model
{
    public class ClaimServiceLinePayment
    {
        public int Id { get; set; }

        public int ClaimPaymentId { get; set; }

        public string ProcedureCode { get; set; }

        public decimal? UnitsPaid { get; set; }

        public DateTime ProcedureDate { get; set; }

        public Money SubmittedAmount { get; set; }

        public Money AllowedAmount { get; set; }

        public decimal? PatientPortion { get; set; }

        public decimal? Deductibles { get; set; }

        public decimal? CoInsurance { get; set; }

        public decimal? Disallowed { get; set; }

        private IList<RemittanceClaimPaymentReasonCode> _reasonCode;


      public string ReasonCodes { get; set; }

        //public IList<RemittanceClaimPaymentReasonCode> ReasonCodes
        //{
        //    get { return _reasonCode ?? new List<RemittanceClaimPaymentReasonCode>(); }
        //    set { _reasonCode = value; }
        //}

        private IList<RemittanceClaimPaymentRemarkCode> _remarkCode;

        public IList<RemittanceClaimPaymentRemarkCode> RemarkCodes
        {
            get { return _remarkCode ?? new List<RemittanceClaimPaymentRemarkCode>(); }
            set { _remarkCode = value; }
        } 
       // public IList<RemittanceClaimPaymentRemarkCode> RemarkCodes { get; set; }

        public decimal? PaidAmount { get; set; }

        //public string RemarksCode { get; set; }

        //public string RemarksDescription { get; set; }


    }
}