using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class ClearinghouseResponse : Entity<string>
    {
        public string transactionID { get; set; }
        public string stRecID { get; set; }
        public string claimRecID { get; set; }
        public string Response { get; set; }
    }
}
