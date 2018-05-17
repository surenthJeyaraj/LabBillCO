using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace Domain.Model
{
    public class RemittanceClaimPayment
    {

        public int Id { get; set; }

        /// <summary>
        ///  This is the ID related to remittance object Primary Id
        /// </summary>
        public Guid RemittanceId { get; set; }

        public string PatientLastName { get; set; }

        public string PatientFirstName { get; set; }

        /// <summary>
        /// Claim number of 837D. It may be null when it cannot be associated with claim
        /// </summary>
        public string ClaimNumber { get; set; }

        /// <summary>
        /// Claim amount can be null when it cannot be associated with claim
        /// </summary>
        public string ClaimAmount { get; set; }

        public string PaidAmount { get; set; }

        public string AccountNumber { get; set; }

        public string PatientIdentificationNumber { get; set; }

        public string ClaimStatusCodeDescription { get; set; }

        public string PayerInternalControlNumber { get; set; }

        public DateTime? DateofService { get; set; }
        

        private IList<RemittanceClaimPaymentReasonCode> _reasonCode;

        public IList<RemittanceClaimPaymentReasonCode> ReasonCodes
        {
            get { return _reasonCode ?? new List<RemittanceClaimPaymentReasonCode>(); }
            set { _reasonCode = value; }
        } 

        private IList<ClaimServiceLinePayment> _serviceLinePayments;

        public IList<ClaimServiceLinePayment> ServiceLinePayments
        {
            get
            {
                return _serviceLinePayments ?? new List<ClaimServiceLinePayment>();
            }
            set { _serviceLinePayments = value; }
        }

    }
}