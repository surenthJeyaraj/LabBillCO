using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
   public  class Patients
    {

        public Guid PatientId { get; set; }
        public string PatientSSN { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public DateTime? DOB { get; set; }
        public OptionSetValue Sex { get; set; }
        public Address PatientAddress { get; set; }

    }
}
