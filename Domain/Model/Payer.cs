using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class Payer
    {
        public string PayerId { get; set; }
        public string PayerName { get; set; }

        public Address PayerAddress;
    }
}
