using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Interfaces
{
    public interface ICRMRequestRepository
    {
        string getResponseData(string MessageID);
        string getMessageID(string MessageID);
    }
}
