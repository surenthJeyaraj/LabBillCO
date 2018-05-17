namespace Domain.Model
{
    public class SearchCriteria
    {
        public string EdiVersion5010A2 { get; set; }
        public string EdiVersion4010A1 { get; set; }
        public string UserId { get; set; }
        public string SubscriberId { get; set; }
        public string ClaimNo { get; set; }
        public string BillingNPI { get; set; }
        public string TransactionModeProd { get; set; }
        public string TransactionModeTest { get; set; }
        public string TransactionType { get; set; }
        public string SubscriberLastName { get; set; }
        public string SubscriberFirstName { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string BillingProviderLastame { get; set; }  
        public string PostedDate { get; set; }
        public string PostedDateFrom { get; set; }
        public string PostedDateTo { get; set; }
        public string PayerId { get; set; }
        public string FileName { get; set; }
        public string PreD { get; set; }
        public string Claim { get; set; }
        public string TransactionNo { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string AckReajection { get; set; }

        public int displayStart { get; set; }
        public int displayLength { get; set; }

        public string sortDir { get; set; }
        public string sortCol { get; set; }
    }
}