using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class PayerResponseDetails
    {
        private IList<LineLevelRejection> _linelevelRejectionInfo;
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string ClaimNumber { get; set; }
        public string ResponseDate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Stc12Value { get; set; }
        public string ResponseTransId { get; set; }
        public string SplitTransId { get; set; }
        public string ProcessingNumber { get; set; }
        public string StRecId { get; set; }
        public string ResponseRecordId { get; set; }

        public PayerResponseDetails()
        {
            _linelevelRejectionInfo=new List<LineLevelRejection>();
        }

        public IList<LineLevelRejection> LinelevelRejectionMessage
        {
            get { return _linelevelRejectionInfo; }
            //set { _linelevelRejectionMessage = value; }
        }

        public void AddLineLevelRejectionInfo(IList<LineLevelRejection> message)
        {
            _linelevelRejectionInfo = message;
        }


    }

    public class LineLevelRejection
    {
        public string ProcedureCode { get; set; }

        public string TotalChargeAmount { get; set; }

        public string TotalPaidAmount { get; set; }

        public string LineStatus { get; set; }

        public string ServiceDate { get; set; }

        public string LineChargeAmount { get; set; }

        public string LineRemarks { get; set; }
    }
    
}
