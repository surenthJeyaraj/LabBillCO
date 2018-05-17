using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Web.Service
{
    public interface IRemittanceReportGenerator
    {
        string GenerateHtml(Remittance remittance);
    }
}
