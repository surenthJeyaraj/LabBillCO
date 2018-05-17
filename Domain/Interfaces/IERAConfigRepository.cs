using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface IERAConfigRepository
    {
        IList<ERAConfiguredPayers> GetERAConfiguredPayers(string dentistId);
        int InsertConfiguredPayers(List<ERAConfiguredPayers> configuredList);
        int RemoveConfiguredPayers(string dentistId);
    }
}
