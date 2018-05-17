using System;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IPayerMasterRepository
    {
        List<PayerList> GetPayerList();
    }
}
