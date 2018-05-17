using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Domain.Model;

namespace Domain.Interfaces
{
    public interface IRemittanceRepository
    {
        IList<RemittanceClaimPayment> GetRemittancePayments(string remittanceId);
        IList<Remittance> GetRemittances(RemittanceSearch remittanceSearch);
      void RemittanceRepository(string connectionstring);
    }
}
