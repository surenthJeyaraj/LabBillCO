using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public abstract class EDIMessage
    {
        /// <summary>
        /// Unique Identifier for a edi message Claimnumber in case of Claim and TRN in case of Eligibility ...
        /// </summary>
        public string Id { get; set; } // ClaimNumber
        /// <summary>
        ///  T
        /// </summary>
        public string PatientID { get; set; }
        public virtual string PatientLastName { get; set; }
        public virtual string PatientFirstName { get; set; }
        public virtual string SubscriberID { get; set; }
        public virtual string SubscriberFirstName { get; set; }
        public virtual string SubscriberLastName { get; set; }
        public virtual string PayerID { get; set; }
     
    }
}
