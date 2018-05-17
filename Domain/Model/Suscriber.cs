using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public class Suscriber
    {
        public OptionSetValue SubscriberRelationship { get; set; }
        public string SubscriberLastName { get; set; }
        public string SubscriberFirstName { get; set; }
        public string SubscriberInsuranceId { get; set; }
        public string SubscriberGroupId { get; set; }
        
    }
}
