using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    //Entities of Remittance(835R)
    public class Remittance
    {
        /// <summary>
        /// Its a primary key auto generation 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Clearinghouse Resposne generation ID
        /// </summary>
        public string RemittanceId { get; set; }

        /// <summary>
        /// Clearinghouse unique id for dentist
        /// </summary>
        public string DentistId { get; set; }

        /// <summary>
        /// Clearinghouse primary billing office name
        /// </summary>
        public string BillingLocationName { get; set; }

        public string ProductCode { get; set; }

        /// <summary>
        /// Check number from 835R
        /// </summary>
        public string CheckNumber { get; set; }

        public int NumberOfClaimsPaid { get; set; }

        public string PayerName { get; set; }

        public string PayeeName { get; set; }

        public decimal PaidAmount { get; set; }

        public DateTime ReceivedDateTime { get; set; }

        public string BillingTaxId { get; set; }

        private IList<RemittanceClaimPayment> _remittanceClaimPayments;

        public IList<RemittanceClaimPayment> RemittanceClaimPayment
        {
            get { return _remittanceClaimPayments ?? (_remittanceClaimPayments = new List<RemittanceClaimPayment>()); }
            set { _remittanceClaimPayments = value; }
        }



    }

    public class RemittanceSearch
    {
        public string PayeeName { get; set; }
        public DateTime? ReceivedDateTimeFrom { get; set; }
        public DateTime? ReceivedDateTimeTo { get; set; }
        public string FileNumber { get; set; }
        public string PayerName { get; set; }
        public string IdetificationCode { get; set; }
        public string PaidAmount { get; set; }
        public string BillingTaxId { get; set; }
    }
}
