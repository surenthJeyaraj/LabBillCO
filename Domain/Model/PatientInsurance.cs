using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class PatientInsurance
    {
        public Patients Patients { get; set; }
        public Suscriber Subscriber;
        public Payer Payer { get; set; }
    }
}
