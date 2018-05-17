using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public enum CrmStatusReason
    {
        Active= 800000001,
        Open= 800000003,
        PendingforApproval= 800000004,
        Processing= 800000005,
        Closed= 800000006,
        Error= 800000007
    }
}
