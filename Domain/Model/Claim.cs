namespace Domain.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Claim : Entity<string>
    {
        /// <summary>
        /// First name of the subscriber
        /// </summary>
        public string SubscriberFirstName { get; set; }
        /// <summary>
        /// Last name of the subscriber
        /// </summary>
        public string SubscriberLastName { get; set; }
        /// <summary>
        /// Subscriber Id 
        /// </summary>
        public string SubscriberId { get; set; }
        /// <summary>
        /// Last name of the patient
        /// </summary>
        public string PatientLastName { get; set; }
        /// <summary>
        /// First Name of the patient
        /// </summary>
        public string PatientFirstName { get; set; }
        /// <summary>
        /// Claim Number 
        /// </summary>
        public string ClaimNumber { get; set; }
        /// <summary>
        /// Payer Id 
        /// </summary>
        public string PayerId { get; set; }
        /// <summary>
        /// Date of service
        /// </summary>
        public string DOS { get; set; }
        /// <summary>
        /// Claim Status
        /// </summary>
        public string ClaimStatus { get; set; }
        /// <summary>
        /// Billing provider last name
        /// </summary>
        public string BillingProviderLastName { get; set; }
        /// <summary>
        /// Rendering provider last name
        /// </summary>
        public string RenderingProviderLastName { get; set; }
        /// <summary>
        /// Billing NPI
        /// </summary>
        public string BillingNPI { get; set; }
        /// <summary>
        /// Claim Type
        /// </summary>
        public string ClaimType { get; set; }
        /// <summary>
        /// ClaimAmount
        /// </summary>
        public int ClaimAmount { get; set; }
        /// <summary>
        /// ClaimRecID 
        /// </summary>
        public int ClaimRecID { get; set; }
        /// <summary>
        /// STRecID
        /// </summary>
        public int STRecID { get; set; }
        /// <summary>
        /// transid
        /// </summary>
        public string transid { get; set; }
        /// <summary>
        /// Clearinghouse Transaction status
        /// </summary>
        public TransactionStatus ClearinghouseStatus { get; set; }
        /// <summary>
        /// Transaction status provided by payer
        /// </summary>
        public TransactionStatus PayerStatus { get; set; }
        /// <summary>
        /// Individual file renderd of the particular claim -ie the xml file
        /// </summary>
        public string ClaimRenderdFileName { get; set; }
        /// <summary>
        /// To check whether claim posted to payer
        /// </summary>
        public string PostedToPayer { get; set; }
        /// <summary>
        /// To check whether response received from payer
        /// </summary>
        public string PayerResponseStatus { get; set; }
        /// <summary>
        /// To check whether response received from payer
        /// </summary>
        public string ch_TraceNo { get; set; }
                
       // public Transaction<Claim> Transaction { get; set; } 
        public Response PayerResponse { get; set; }
       
    }
}