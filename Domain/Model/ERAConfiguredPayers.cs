using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Model
{
    public class ERAConfiguredPayers
    {
        public string DentistId { get; set; }
        public string PayerId { get; set; }
        public string SelPayerId { get; set; }
        public string PayerName { get; set; }
    }

    public class ERA_PAYER_PROVIDER_MAP
    {
        public string DentistId { get; set; }
        public string PayerId { get; set; }
    }
}
